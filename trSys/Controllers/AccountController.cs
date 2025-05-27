using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using trSys.DTOs;
using trSys.Interfaces;

namespace trSys.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        IUserService userService,
        ILogger<AccountController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        try
        {
            var result = await _userService.LoginAsync(dto);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, result.User!.Id.ToString()),
                new(ClaimTypes.Email, result.User.Email),
                new(ClaimTypes.Name, result.User.FullName),
                new(ClaimTypes.Role, result.User.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = dto.RememberMe // Установка свойства IsPersistent
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при входе в систему");
            ModelState.AddModelError(string.Empty, "Произошла ошибка при входе");
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied() => View();
}
