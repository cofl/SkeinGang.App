using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SkeinGang.AdminUI.Models;

public record PlayerDto
{
    [HiddenInput, FromForm, DataType(DataType.Text)]
    public long? Id { get; init; }
    
    [FromForm]
    public required string GameAccount { get; init; }
    
    [FromForm]
    public required string DiscordAccountName { get; init; }
    
    [FromForm, DataType(DataType.Text)]
    public long? DiscordAccountId { get; init; }
    
    [BindNever, DataType(DataType.Text)]
    public int? MembershipCount { get; init; }
}