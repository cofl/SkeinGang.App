using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SkeinGang.AdminUI.Models;
using SkeinGang.AdminUI.Services;

namespace SkeinGang.AdminUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/admin-ui/statics")]
public class TeamsController(
    TeamService teams
): Controller
{
    [HttpGet]
    public IActionResult Index()
        => View(teams.FindAll());
    
    [HttpGet]
    [Route("edit/{teamId:long}")]
    public IActionResult EditTeam(long teamId) => throw new NotImplementedException();

    [HttpGet]
    [Route("create")]
    public IActionResult Create()
        => View();
    
    [HttpGet]
    [Route("members/find-user")]
    public IActionResult FindUser() => throw new NotImplementedException();
    
    [HttpPost]
    [Route("updateStatic")]
    public IActionResult UpdateTeam() => throw new NotImplementedException();
    
    [HttpPost]
    [Route("updateStaticRow")]
    public IActionResult UpdateTeamRow() => throw new NotImplementedException();

    [HttpPost]
    [Route("createStatic")]
    public IActionResult CreateTeam(TeamDto team)
    {
        var created = teams.Create(team);
        return this.SeeOther(Url.Action("EditTeam", "Teams", values: new { teamId = created.TeamId })!);
    }
    
    [HttpGet]
    [Route("{teamId:long}/memberSearch")]
    public IActionResult FindTeamMembers(long teamId, [FromQuery] string playerName) => throw new NotImplementedException();
    
    [HttpPut]
    [Route("{teamId:long}/members/{memberId:long}")]
    public IActionResult UpdateTeamMember(long teamId, long memberId) => throw new NotImplementedException();
    
    [HttpDelete]
    [Route("{teamId:long}/members/{memberId:long}")]
    public IActionResult DeleteTeamMember(long teamId, long memberId) => throw new NotImplementedException();
    
    [HttpPost]
    [Route("{teamId:long}/members")]
    public IActionResult AddTeamMember(long teamId) => throw new NotImplementedException();
    
    [HttpPost]
    [Route("{teamId:long}/membersCreateAndAdd")]
    public IActionResult CreateAndAddMember(long teamId) => throw new NotImplementedException();
}