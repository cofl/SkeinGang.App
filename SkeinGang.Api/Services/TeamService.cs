using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SkeinGang.Api.Models;
using SkeinGang.Data.Context;

namespace SkeinGang.Api.Services;

file static class TeamQueryableExtensions
{
    internal static IQueryable<Domain.Team> IncludeTeamDtoRelated(this IQueryable<Domain.Team> teams)
        => teams.Include(t => t.TeamDetail);

    internal static IQueryable<Domain.Team> IncludeTeamWithMembersDtoRelated(this IQueryable<Domain.Team> teams) =>
        teams
            .Include(t => t.TeamDetail)
            .Include(t => t.TeamMemberships)
            .ThenInclude(m => m.Player);
}

public class TeamService(DataContext dataContext)
{
    internal ResultDto<TeamDto> FindAll(int limit, int offset, IEnumerable<TeamFilter> filters)
    {
        var teams = filters
            .Aggregate<TeamFilter, IQueryable<Domain.Team>>(dataContext.Teams,
                (teams, filter) => teams.Where(filter.Filter))
            .OrderBy(t => t.TeamId)
            .Skip(offset)
            .Take(limit + 1)
            .IncludeTeamDtoRelated()
            .Select(t => new TeamDto(t))
            .ToList();
        return new ResultDto<TeamDto>(teams.Count > limit, teams[..Math.Min(teams.Count, limit)]);
    }

    internal ResultDto<TeamDto> GetAll(List<long> ids)
    {
        var teams = dataContext.Teams
            .Where(t => ids.Contains(t.TeamId))
            .IncludeTeamDtoRelated()
            .Select(t => new TeamDto(t))
            .ToList();
        return new ResultDto<TeamDto>(false, teams);
    }

    internal bool TryGetTeam(long teamId, [NotNullWhen(true)] out TeamWithMembersDto? team)
    {
        var result = dataContext.Teams
            .IncludeTeamWithMembersDtoRelated()
            .FirstOrDefault(t => t.TeamId == teamId);
        team = result is null ? null : new TeamWithMembersDto(result);
        return result != null;
    }

    internal bool TryGetTeam(string slug, [NotNullWhen(true)] out TeamWithMembersDto? team)
    {
        var result = dataContext.Teams
            .IncludeTeamWithMembersDtoRelated()
            .FirstOrDefault(t => t.Slug == slug);
        team = result is null ? null : new TeamWithMembersDto(result);
        return result != null;
    }
}
