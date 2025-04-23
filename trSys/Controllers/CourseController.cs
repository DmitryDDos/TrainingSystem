using Microsoft.AspNetCore.Mvc;
using trSys.Interfaces;
using trSys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace trSys.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CourseController : BaseController<Course>
    {
        public CourseController(IRepository<Course> courseRepository) : base(courseRepository)
        {
        }
    }
}
