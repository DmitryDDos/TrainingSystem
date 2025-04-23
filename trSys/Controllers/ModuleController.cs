using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId}/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ModuleController : BaseController<Module>
    {
        public ModuleController(IRepository<Module> moduleRepository) : base(moduleRepository)
        {
        }
    }
}
