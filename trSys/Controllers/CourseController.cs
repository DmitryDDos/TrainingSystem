using Microsoft.AspNetCore.Mvc;
using trSys.Interfaces;
using trSys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace trSys.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : BaseController<Course>
    {
        public CourseController(IRepository<Course> repository) : base(repository)
        {
        }

        // Методы курсов
    }
}
