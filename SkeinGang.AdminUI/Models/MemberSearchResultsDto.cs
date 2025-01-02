namespace SkeinGang.AdminUI.Models;

public class MemberSearchResultsDto
{
    public required long TeamId { get; set; }
    public required List<PlayerDto> Players { get; set; }
}
