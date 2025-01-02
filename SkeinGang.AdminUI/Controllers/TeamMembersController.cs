using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SkeinGang.AdminUI.Models;
using SkeinGang.AdminUI.Services;
using SkeinGang.Data.Enums;

namespace SkeinGang.AdminUI.Controllers;

[Route("/admin-ui/statics/{teamId:long}")]
public class TeamMembersController(TeamService teams, TeamMemberService members) : Controller
{
    [HttpGet("/admin-ui/statics/members/find-user")]
    public IActionResult FindUser(long teamId)
        => throw new NotImplementedException();

    [HttpGet("memberSearch")]
    public IActionResult FindTeamMembers(long teamId, [FromQuery] string playerName)
        => throw new NotImplementedException();

    [HttpPost("members")]
    [Consumes(MediaTypeNames.Application.FormUrlEncoded)]
    public IActionResult AddTeamMember(long teamId, PlayerDto player)
    {
        members.Add(teamId, player);
        return PartialView("Components/MembersForm", teams.FindWithMembersById(teamId));
    }

    [HttpPost("membersCreateAndAdd")]
    public IActionResult CreateAndAddTeamMember(long teamId)
        => throw new NotImplementedException();

    [HttpPut("members/{memberId:long}")]
    [Consumes(MediaTypeNames.Application.FormUrlEncoded)]
    public IActionResult UpdateTeamMember(long teamId, long memberId,
        [FromForm, Required] MembershipType membershipType)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        members.UpdateMembershipType(teamId, memberId, membershipType);
        return PartialView("Components/MembersForm", teams.FindWithMembersById(teamId));
    }

    [HttpDelete("members/{memberId:long}")]
    public IActionResult DeleteTeamMember(long teamId, long memberId)
    {
        members.Delete(teamId, memberId);
        return PartialView("Components/MembersForm", teams.FindWithMembersById(teamId));
    }
}
