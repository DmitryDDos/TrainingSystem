using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;

public class LoginModel : PageModel
{
    private readonly IUserService _userService;

    [BindProperty]
    public LoginDto Input { get; set; }

    public LoginModel(IUserService userService)
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

        var result = await _userService.LoginAsync(Input);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return Page();
        }

        Response.Cookies.Append("auth_token", result.Token);
        return RedirectToPage("/Index");
    }
}
