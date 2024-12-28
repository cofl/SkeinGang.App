using Microsoft.AspNetCore.Mvc;

namespace SkeinGang.AdminUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/admin-ui/[controller]")]
public class PlayersController : Controller
{
    [HttpGet]
    public IActionResult Index() => throw new NotImplementedException();
    
    [HttpGet("[action]")]
    public IActionResult Create() => throw new NotImplementedException();
    
    [HttpGet("[action]/{playerId:long}")]
    public IActionResult EditPlayer(long playerId) => throw new NotImplementedException();
    
    [HttpPost("[action]")]
    public IActionResult CreatePlayer() => throw new NotImplementedException();
    
    [HttpPost("[action]")]
    public IActionResult UpdatePlayer() => throw new NotImplementedException();
}