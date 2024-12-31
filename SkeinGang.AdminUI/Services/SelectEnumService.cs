using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using NodaTime;
using SkeinGang.Data.Enums;

namespace SkeinGang.AdminUI.Services;

public class SelectEnumService(IOptions<JsonOptions> jsonOptions)
{
    private readonly Dictionary<Type, List<SelectListItem>> _options = [];

    private static List<string> DefaultTimeZones { get; } =
    [
        "Europe/Brussels",
        "America/New_York",
        "America/Los_Angeles",
        "Australia/Sydney",
        "UTC"
    ];

    // ReSharper disable once MemberCanBeMadeStatic.Global
#pragma warning disable CA1822
    internal List<SelectListItem> AsSelectOptions(DateTimeZone zone) =>
        DefaultTimeZones
            .Select(a => new SelectListItem(a, a, a == zone.Id))
            .ToList();
#pragma warning restore CA1822

    internal List<SelectListItem> AsSelectOptions<T>(T selected)
        where T : struct, Enum
    {
        return GetOptions<T>(selected);
    }

    private List<SelectListItem> GetOptions<T>(T? selected) where T : struct, Enum
    {
        var selectedValue = GetValue(selected) is { } item ? GetValue(item) : null;
        return Enum.GetValues<T>()
            .Select(a =>
            {
                var value = GetValue(a);
                ArgumentNullException.ThrowIfNull(value);
                return new SelectListItem
                {
                    Text = GetName(a),
                    Value = value,
                    Selected = value == selectedValue
                };
            })
            .ToList();
    }

    private string? GetValue<T>(T value) => value switch
    {
        null => null,
        _ => JsonSerializer.Deserialize<string>(
            JsonSerializer.Serialize<T>(value, jsonOptions.Value.JsonSerializerOptions))
    };

    private static string GetName(Region region) => region switch
    {
        Region.NorthAmerica => "North America",
        Region.Europe => "Europe",
        Region.Australia => "Australia",
    };

    private static string GetName<T>(T item) where T : struct, Enum
        => item switch
        {
            ContentDifficulty difficulty => difficulty switch
            {
                ContentDifficulty.NormalMode => "NM",
                ContentDifficulty.ChallengeMode => "CM",
                ContentDifficulty.LegendaryMode => "LM",
            },
            ContentFocus focus => focus switch
            {
                ContentFocus.HeartOfThorns => "HoT",
                ContentFocus.PathOfFire => "PoF",
                ContentFocus.HeartOfThornsAndPathOfFire => "HoT & PoF",
                ContentFocus.EndOfDragons => "EoD Strikes",
                ContentFocus.Fractals => "Fractals",
                ContentFocus.HarvestTemple => "HT",
                ContentFocus.TempleOfFebe => "ToF",
                ContentFocus.Competitive => "Competitive",
                ContentFocus.Social => "Social",
                ContentFocus.JanthirWilds => "Janthir Wilds",
            },
            ExperienceLevel rating => rating.ToString(),
            IsoDayOfWeek dayOfWeek => dayOfWeek.ToString(),
            _ => typeof(T)
                     .GetProperty(item.ToString())
                     ?.GetCustomAttribute<EnumMemberAttribute>()
                     ?.Value
                 ?? item.ToString()
        };
}
