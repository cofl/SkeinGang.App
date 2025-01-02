using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkeinGang.AdminUI.Models;
using SkeinGang.AdminUI.Services;

namespace SkeinGang.AdminUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/admin-ui/[controller]")]
[Authorize(Roles = AdminUIRoles.Moderator)]
public class PlayersController(PlayerService players) : Controller
{
    internal const string GameAccountField = "gw2Name";

    [HttpGet]
    public IActionResult Index(
        [FromQuery(Name = GameAccountField)] string? gameAccount,
        PlayerFilter filter = PlayerFilter.All
    ) =>
        View(filter switch
        {
            PlayerFilter.Incomplete => players.FindIncomplete(),
            PlayerFilter.Name => players.FindAll(gameAccount),
            PlayerFilter.All when Request.Query.ContainsKey(GameAccountField) => players.FindAll(gameAccount),
            _ => null,
        });

    [HttpGet("[action]")]
    public IActionResult Create()
        => View();

    [HttpGet("[action]/{playerId:long}")]
    public IActionResult EditPlayer(long playerId)
        => players.FindById(playerId) is { } player
            ? View(player)
            : NotFound();

    [HttpPost("editPlayer/{playerId:long}")]
    public IActionResult PostEditPlayer(long playerId, PlayerDto player)
        => playerId != player.Id
            ? BadRequest("Route must match target player.")
            : View(nameof(EditPlayer), players.Update(player));

    [HttpPost("[action]")]
    public IActionResult CreatePlayer(PlayerDto player)
    {
        var created = players.Create(player);
        return this.SeeOther(Url.Action("EditPlayer", "Players", new { playerId = created.Id })!);
    }

    [HttpPost("[action]")]
    public void UpdatePlayer(PlayerDto player)
    {
        if(player.Id is null)
            players.Create(player);
        else
            players.Update(player);
    }
}