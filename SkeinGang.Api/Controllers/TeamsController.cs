using Microsoft.AspNetCore.Mvc;
using NodaTime;
using SkeinGang.Api.Controllers.Parameters;
using SkeinGang.Api.Models;
using SkeinGang.Api.Services;
using SkeinGang.Api.Util;

namespace SkeinGang.Api.Controllers;

/// <summary>
///     Controller for teams.
/// </summary>
/// <param name="teams">Service for interfacing with teams.</param>
/// <param name="highlighted">Service for interfacing with team highlights.</param>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TeamsController(TeamService teams, HighlightService highlighted) : ControllerBase
{
    /// <summary>
    ///     Get all teams.
    /// </summary>
    /// <param name="parameters">Optional filters for the query.</param>
    /// <returns>A result containing a (possibly partial) list of results.</returns>
    /// <remarks>
    ///     Get all teams with optional filters. By default, archived teams are excluded.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType<ResultDto<TeamDto>>(StatusCodes.Status200OK)]
    public ResultDto<TeamDto> Index(TeamIndexParameters parameters) =>
        teams.FindAll(parameters.Limit, parameters.Offset, new NonNullList<TeamFilter>
        {
            parameters.Region.IfNotEmpty(a => new TeamFilter.Regions(a)),
            parameters.ContentType.IfNotEmpty(a => new TeamFilter.ContentFocuses(a)),
            parameters.Difficulty.IfNotEmpty(a => new TeamFilter.DifficultyLevels(a)),
            parameters.Rating.IfNotEmpty(a => new TeamFilter.ExperienceLevels(a)),
            parameters.Day.IfNotEmpty(a => new TeamFilter.DaysOfWeek(a)),
            parameters.Archived switch
            {
                AnyBoolean.True => new TeamFilter.Archived(true),
                AnyBoolean.False => new TeamFilter.Archived(false),
                _ => null,
            },
            parameters.Time is { } time
                ? new TeamFilter.TimesOfDay(time, parameters.UntilTime)
                : null,
            parameters.HasDuration
                ? new TeamFilter.Durations(
                    Duration.FromSeconds(parameters.Duration ?? 0),
                    Duration.FromSeconds(parameters.MaxDuration ?? parameters.Duration ?? 0))
                : null,
            parameters.HasSlots
                ? new TeamFilter.HasEmptySlots(parameters.MinSlots ?? 0,
                    parameters.MaxSlots ?? TeamIndexParameters.MaximumTeamCapacity)
                : null,
        });

    /// <summary>
    ///     Get a team by ID number.
    /// </summary>
    /// <param name="teamId">The ID of the team to fetch.</param>
    /// <returns>The team, with detailed member information.</returns>
    [HttpGet("{teamId:long}")]
    [ProducesResponseType<TeamWithMembersDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long teamId)
        => teams.TryGetTeam(teamId, out var team) ? Ok(team) : NotFound();

    /// <summary>
    ///     Get a team by name.
    /// </summary>
    /// <param name="slug">The globally-unique name of the team to fetch.</param>
    /// <returns>The team, with detailed member information.</returns>
    [HttpGet("named/{slug}")]
    [ProducesResponseType<TeamWithMembersDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetBySlug(string slug)
        => teams.TryGetTeam(slug, out var team) ? Ok(team) : NotFound();

    /// <summary>
    ///     Get highlighted teams.
    /// </summary>
    /// <returns>A result containing all highlighted teams in a list.</returns>
    [HttpGet("highlighted")]
    public ResultDto<TeamDto> GetHighlighted() =>
        teams.GetAll(highlighted.CurrentlyHighlighted);

    /// <summary>
    ///     Empty the list of highlighted teams.
    /// </summary>
    /// <remarks>
    ///     Clear the list of highlighted teams. The next GET request to this endpoint will repopulate the list.
    /// </remarks>
    [HttpDelete("highlighted")]
    public void DeleteHighlighted() =>
        highlighted.RemoveAllCurrentlyHighlighted();
}
