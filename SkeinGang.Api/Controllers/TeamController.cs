using Microsoft.AspNetCore.Mvc;
using SkeinGang.Api.Services;
using SkeinGang.Api.Views;

namespace SkeinGang.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TeamsController(TeamService teams, HighlightService highlighted) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<ResultDto<TeamDto>>(StatusCodes.Status200OK)]
    public IActionResult Index()
    {
        return Ok(teams.FindAll(100, 0, Array.Empty<TeamFilter>()));
    }

    [HttpGet, Route("{teamId:long}")]
    [ProducesResponseType<TeamWithMembersDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long teamId)
        => teams.TryGetTeam(teamId, out var team) ? Ok(team) : NotFound();

    [HttpGet, Route("named/{slug}")]
    [ProducesResponseType<TeamWithMembersDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetBySlug(string slug)
        => teams.TryGetTeam(slug, out var team) ? Ok(team) : NotFound();

    [HttpGet, Route("highlighted")]
    public ResultDto<TeamDto> GetHighlighted()
    {
        return teams.GetAll(highlighted.CurrentlyHighlighted);
    }

    [HttpDelete, Route("highlighted")]
    public void DeleteHighlighted()
    {
        highlighted.RemoveAllCurrentlyHighlighted();
    }
}
