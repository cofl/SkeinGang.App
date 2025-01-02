using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using SkeinGang.AdminUI.Models;
using SkeinGang.AdminUI.Services;
using SkeinGang.Data.Enums;

namespace SkeinGang.AdminUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/admin-ui/statics")]
public class TeamsController(TeamService teams): Controller
{
    [HttpGet]
    public IActionResult Index()
        => View(teams.FindAll());
    
    [HttpGet("create")]
    public IActionResult Create()
        => View(new TeamDto
        {
            // default values for the form.
            Name = "",
            TimeZone = DateTimeZone.Utc,
            Region = Region.NorthAmerica,
            ContentFocus = ContentFocus.HeartOfThorns,
            ContentDifficulty = ContentDifficulty.NormalMode,
            ExperienceLevel = ExperienceLevel.Progression,
            DayOfRaid = IsoDayOfWeek.None,
            TimeOfRaid = LocalTime.FromHoursSinceMidnight(18), // 6pm
            DiscordServerId = 1,
            HoldSlots = 0,
        });
    
    [HttpGet("edit/{teamId:long}")]
    public IActionResult Edit(long teamId)
        => teams.FindWithMembersById(teamId) is {} team
            ? View(team)
            : NotFound();
    
    [HttpPost("createStatic")]
    public IActionResult CreateTeam(TeamDto team)
    {
        var created = teams.Create(team);
        return this.SeeOther(Url.Action("Edit", "Teams", values: new { teamId = created.TeamId })!);
    }

    [HttpPost("updateStatic")]
    [Consumes(MediaTypeNames.Application.FormUrlEncoded)]
    public void UpdateTeam(TeamDto team)
        => teams.Update(team);
}