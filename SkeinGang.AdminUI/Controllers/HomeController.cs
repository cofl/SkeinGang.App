using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkeinGang.AdminUI.Models;

namespace SkeinGang.AdminUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/admin-ui")]
[Authorize(Roles = AdminUIRoles.Moderator)]
public class HomeController(IConfiguration config) : Controller
{
    private string ModeratorUsername => config["MODERATOR_USER"] ?? throw new ArgumentNullException(nameof(ModeratorUsername), "MODERATOR_USER must be set.");
    private string ModeratorPassword => config["MODERATOR_PASSWORD"] ?? throw new ArgumentNullException(nameof(ModeratorPassword), "MODERATOR_PASSWORD must be set.");
    
    [HttpGet]
    public IActionResult Index() => View();
    
    [HttpGet("[action]")]
    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost(nameof(Login))]
    [AllowAnonymous]
    public async Task<IActionResult> PostLoginAsync(LoginForm form, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(nameof(Login));
        if (form.Username != ModeratorUsername || form.Password != ModeratorPassword)
        {
            ModelState.AddModelError(nameof(form), "Invalid username or password.");
            return View(nameof(Login));
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, ModeratorUsername),
            new(ClaimTypes.Role, AdminUIRoles.Moderator),
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return LocalRedirect(returnUrl ?? Url.Action("Index", "Home")!);
    }

    [HttpGet("[action]"), HttpPost("[action]")]
    public async Task<IActionResult> LogoutAsync(string? returnUrl = null)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect(returnUrl ?? Url.Action("Index", "Home")!);
    }
}
