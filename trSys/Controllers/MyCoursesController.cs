using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trSys.Models;

namespace trSys.Controllers
{
    public class MyCoursesController : Controller
    {
        [Authorize]
        public IActionResult MyCourses()

        {
            var model = new MyCourses
            {
                FullName = "Иванов Иван Иванович"
            };

            return View("~/Views/Account/MyCourses.cshtml", model);
        }
    }
}