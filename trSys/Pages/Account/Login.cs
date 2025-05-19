using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILoginService _loginService;

        [BindProperty]
        public LoginDto Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public string ReturnUrl { get; set; }

        public LoginModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public void OnGet(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _loginService.LoginAsync(Input);

            if (result.Success)
            {
                return LocalRedirect(ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, result.Message);
            return Page();
        }
    }
}
