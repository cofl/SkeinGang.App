using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Context;

namespace SkeinGang.AdminUI.Services;

public class TeamService(DataContext context)
{
    public List<TeamDto> FindAll()
        => context.Teams
            .OrderBy(t => t.Name)
            .ProjectToDto()
            .ToList();
}