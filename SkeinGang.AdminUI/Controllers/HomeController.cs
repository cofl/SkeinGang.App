using Microsoft.AspNetCore.Mvc;

namespace SkeinGang.AdminUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index() => throw new NotImplementedException();
    
    [HttpGet("[action]")]
    public IActionResult Login() => throw new NotImplementedException();
    
    [HttpPost("[action]")]
    public IActionResult Logout() => throw new NotImplementedException();
}
