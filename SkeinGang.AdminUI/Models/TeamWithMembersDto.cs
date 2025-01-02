namespace SkeinGang.AdminUI.Models;

public record TeamWithMembersDto: TeamDto
{
    public required List<TeamMemberDto> Members { get; set; }
}
