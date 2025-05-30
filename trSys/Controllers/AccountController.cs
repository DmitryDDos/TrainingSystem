using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using trSys.DTOs;
using trSys.Interfaces;

namespace trSys.Controllers;

public class AccountController : Controller
{
    private readonly ICourseRegistrationService _registrationService;
    private readonly IUserService _userService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        IUserService userService,
        ILogger<AccountController> logger,
        ICourseRegistrationService registrationService)
    {
        _userService = userService;
        _logger = logger;
        _registrationService = registrationService;
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

    [HttpGet]
<<<<<<< HEAD
=======
    [Authorize]
>>>>>>> bfbc1eb6d618c9deec3af42379f369f40a9498b8
    public async Task<IActionResult> MyCourses()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var courses = await _registrationService.GetUserCoursesAsync(userId);
        return View(courses);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            // Получаем ID текущего администратора
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userService.RegisterUserAsync(dto, adminId);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["SuccessMessage"] = "Пользователь успешно зарегистрирован";
            return RedirectToAction("Users", "Admin"); // Перенаправление на список пользователей
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка регистрации пользователя");
            ModelState.AddModelError(string.Empty, "Ошибка при регистрации");
            return RedirectToAction("Register", "Account");
        }
    }
}
