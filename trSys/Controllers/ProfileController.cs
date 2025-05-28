using Microsoft.AspNetCore.Mvc;
using trSys.Models;

namespace trSys.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            var model = new Profile
            {
                FullName = "Иванов Иван Иванович",
                Email = "ivanov@example.com",
                Phone = "+7 (999) 123-45-67"
            };

            return View("~/Views/Account/Profile.cshtml", model);
        }
    }
}
