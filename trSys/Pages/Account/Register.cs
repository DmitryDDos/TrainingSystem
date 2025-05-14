using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using trSys.DTOs;
using trSys.Interfaces;

namespace trSys.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly IUserService _userService;

    [BindProperty]
    public RegisterDto Input { get; set; }

    public RegisterModel(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _userService.RegisterAsync(Input);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return Page();
        }

        // Автоматический вход после регистрации
        Response.Cookies.Append("auth_token", result.Token);
        return RedirectToPage("/Index");
    }
}
