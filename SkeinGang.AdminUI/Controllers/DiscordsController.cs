using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkeinGang.AdminUI.Models;
using SkeinGang.AdminUI.Services;

namespace SkeinGang.AdminUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/admin-ui/[controller]")]
[Authorize(Roles = AdminUIRoles.Moderator)]
public class DiscordsController(DiscordServerService discords) : Controller
{
    [HttpGet]
    public IActionResult Index() =>
        View(discords.FindAll());

    [HttpGet("[action]")]
    public IActionResult Create() =>
        View();

    [HttpPost("[action]")]
    public IActionResult CreateDiscordServer(DiscordServerDto serverDto)
    {
        discords.Create(serverDto);
        return this.SeeOther(Url.Action("Index", "Discords")!);
    }

    [HttpPost("[action]")]
    public void UpdateDiscordServer(DiscordServerDto serverDto)
    {
        if (serverDto.Id is null)
            discords.Create(serverDto);
        else
            discords.Update(serverDto);
    }
}
