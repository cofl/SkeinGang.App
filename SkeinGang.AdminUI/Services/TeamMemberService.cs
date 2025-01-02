using Microsoft.AspNetCore.Mvc;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Context;
using SkeinGang.Data.Enums;

namespace SkeinGang.AdminUI.Services;

public class TeamMemberService(DataContext context)
{
    internal void UpdateMembershipType(long teamId, long memberId, MembershipType newType)
    {
        Console.WriteLine($"Updating membership type {memberId} to {newType}");
        var member = context.TeamMemberships
            .Single(m  => m.Id == memberId && m.StaticId == teamId);
        member.MembershipType = newType;
        context.Update(member);
        context.SaveChanges();
    }

    internal void Delete(long teamId, long memberId)
    {
        var member = context.TeamMemberships
            .Single(m => m.Id == memberId && m.StaticId == teamId);
        context.TeamMemberships.Remove(member);
        context.SaveChanges();
    }

    internal void Add(long teamId, PlayerDto player)
    {
        throw new NotImplementedException();
    }
}
