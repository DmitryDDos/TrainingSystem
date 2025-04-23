using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace trSys.Models
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formParameters = context.MethodInfo.GetParameters()
                .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(FromFormAttribute)));

            if (!formParameters.Any()) return;

            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = formParameters.ToDictionary(
                            p => p.Name,
                            p => GetSchemaForParameter(p)
                        ),
                        Required = formParameters
                            .Where(p => !IsNullable(p.ParameterType))
                            .Select(p => p.Name)
                            .ToHashSet()
                    }
                }
            }
            };
        }

        private OpenApiSchema GetSchemaForParameter(ParameterInfo parameter)
        {
            return parameter.ParameterType == typeof(IFormFile)
                ? new OpenApiSchema { Type = "string", Format = "binary" }
                : new OpenApiSchema { Type = "string" };
        }

        private bool IsNullable(Type type) =>
            Nullable.GetUnderlyingType(type) != null ||
            (type.IsClass && type != typeof(string));
    }

}
