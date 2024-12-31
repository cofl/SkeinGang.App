using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Context;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Services;

public class TeamService(DataContext context)
{
    public List<TeamDto> FindAll()
        => context.Teams
            .OrderBy(t => t.Name)
            .ProjectToDto()
            .ToList();

    public TeamDto Create(TeamDto team)
    {
        if (team.TeamId != null)
            throw new ArgumentException(
                paramName: nameof(team),
                message: $"{nameof(team.TeamId)} must be null."
            );
        var model = context.Teams.Add(team.ToEntity());
        context.SaveChanges();
        return model.EntityWithDtoRelated().ToDto();
    }

    public TeamDto Update(TeamDto team)
    {
        var model = context.Teams
            .AsTracking()
            .IncludeTeamDtoRelated()
            .First(entity => entity.TeamId == team.TeamId);
        model.ApplyUpdate(team);
        return model.ToDto();
    }
}

file static class TeamServiceExtensions
{
    internal static IQueryable<Team> IncludeTeamDtoRelated(this IQueryable<Team> teams)
        => teams.Include(t => t.TeamDetail);

    internal static Team EntityWithDtoRelated(this EntityEntry<Team> entry)
    {
        entry.Reference(entity => entity.TeamDetail).Load();
        return entry.Entity;
    }
}