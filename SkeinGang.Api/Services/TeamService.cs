using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SkeinGang.Api.Views;
using SkeinGang.Data.Context;

namespace SkeinGang.Api.Services;

public class TeamService(DataContext dataContext)
{
    public ResultDto<TeamDto> FindAll(int limit, int offset, IEnumerable<TeamFilter> filters)
    {
        var teams = filters
            .Aggregate((IQueryable<Domain.Team>)dataContext.Teams, (teams, filter) => teams.Where(filter.Filter))
            .Skip(offset)
            .Take(limit + 1)
            .Select(t => new TeamDto(t))
            .ToList();
        return new ResultDto<TeamDto>(false, teams);
    }
    
    public IQueryable<Domain.Team> GetAll(List<long> ids) =>
        dataContext.Teams.Where(t => ids.Contains(t.TeamId));

    public bool TryGetTeam(long teamId, [NotNullWhen(returnValue: true)] out TeamWithMembersDto? team)
    {
        var result = dataContext.Teams
            .Include(t => t.TeamDetail)
            .Include(t => t.TeamMemberships)
            .ThenInclude(t => t.Player)
            .FirstOrDefault(t => t.TeamId == teamId);
        team = result is null ? null : new TeamWithMembersDto(result);
        return result != null;
    }
    
    public bool TryGetTeam(string slug, [NotNullWhen(returnValue: true)] out TeamWithMembersDto? team)
    {
        var result = dataContext.Teams
            .Include(t => t.TeamDetail)
            .Include(t => t.TeamMemberships)
            .ThenInclude(t => t.Player)
            .FirstOrDefault(t => t.Slug == slug);
        team = result is null ? null : new TeamWithMembersDto(result);
        return result != null;
    }
}
