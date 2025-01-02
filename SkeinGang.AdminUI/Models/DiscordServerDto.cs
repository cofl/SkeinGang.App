using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SkeinGang.AdminUI.Models;

public record DiscordServerDto
{
    [HiddenInput]
    [FromForm]
    public required long? Id { get; init; }

    [FromForm]
    public required string? ServerName { get; init; }

    [FromForm]
    [DataType(DataType.Text)]
    public required long? ServerId { get; init; }
}
