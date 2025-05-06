using trSys.DTOs;
using trSys.Models;

namespace trSys.Mappers
{
    public static class CourseMapper
    {
        public static CourseDto ToDto(Course course) => new(
            course.Id, course.Title, course.Description);

        public static CourseDetailsDto ToDetailsDto(
            Course course, IEnumerable<Module> modules) => new(
            course.Id,
            course.Title,
            course.Description,
            modules.Select(m => ModuleMapper.ToDto(m)));
    }
}
