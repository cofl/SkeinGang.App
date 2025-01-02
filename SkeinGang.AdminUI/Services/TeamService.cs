using Microsoft.EntityFrameworkCore;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data;
using SkeinGang.Data.Context;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Services;

public class TeamService(DataContext context)
{
    internal List<TeamDto> FindAll()
        => context.Teams
            .OrderBy(t => t.Name)
            .ProjectToDto()
            .ToList();
    
    internal TeamDto? FindById(long teamId)
        => context.Teams
            .AsNoTracking()
            .ProjectToDto()
            .FirstOrDefault(team => team.TeamId == teamId);
    
    public TeamWithMembersDto? FindWithMembersById(long teamId)
        => context.Teams
            .AsNoTracking()
            .FirstOrDefault(team => team.TeamId == teamId)
            ?.ToWithMembersDto();
    
    internal TeamDto Create(TeamDto team)
    {
        Assert.MustBeNull(team.TeamId);
        
        var model = context.Teams.AddNew(team.ToEntity());
        context.SaveChanges();
        return model.ToDto();
    }

    internal TeamDto Update(TeamDto team)
    {
        Assert.MustNotBeNull(team.TeamId);
        
        var model = context.Teams
            .AsTracking()
            .IncludeTeamDtoRelated()
            .First(entity => entity.TeamId == team.TeamId);
        model.ApplyUpdate(team);
        context.SaveChanges();
        return model.ToDto();
    }
}

file static class TeamServiceExtensions
{
    internal static IQueryable<Team> IncludeTeamDtoRelated(this IQueryable<Team> teams)
        => teams.Include(t => t.TeamDetail);
    
    internal static IQueryable<Team> IncludeTeamWithMembersDtoRelated(this IQueryable<Team> teams) =>
        teams
            .Include(t => t.TeamDetail)
            .Include(t => t.TeamMemberships)
            .ThenInclude(m => m.Player);
}