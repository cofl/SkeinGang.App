using Microsoft.AspNetCore.Mvc;

namespace SkeinGang.AdminUI.Controllers;

[Route("app/test")]
[Produces("application/json")]
public class TestController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Message"] = "Hello World!";
        return View();
    }
}
