using Microsoft.AspNetCore.Mvc;

namespace SkeinGang.AdminUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/admin-ui/[controller]")]
public class DiscordsController : Controller
{
    [HttpGet]
    public IActionResult Index() => throw new NotImplementedException();
    
    [HttpGet("[action]")]
    public IActionResult Create() => throw new NotImplementedException();
    
    [HttpPost("[action]")]
    public IActionResult CreateDiscordServer() => throw new NotImplementedException();
    
    [HttpPost("[action]")]
    public IActionResult UpdateDiscordServer() => throw new NotImplementedException();
}
