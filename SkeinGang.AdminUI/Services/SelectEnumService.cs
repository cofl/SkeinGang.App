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
    private record EnumOption(string Text, string Value);
    private readonly Dictionary<Type, List<EnumOption>> _options = [];

    internal List<SelectListItem> AsSelectOptions<T>(T selected)
        where T : struct, Enum
    {
        var selectedValue = JsonSerializer.Serialize(selected);
        if(!_options.TryGetValue(typeof(T), out var options))
            options = _options[typeof(T)] = GetOptions<T>();
        return options
            .Select(item => new SelectListItem(
                item.Text,
                item.Value,
                item.Value == selectedValue
            ))
            .ToList();
    }
    
    private List<EnumOption> GetOptions<T>() where T : struct, Enum
    {
        return Enum.GetValues<T>()
            .Select(a => new EnumOption(
                a.GetCustomName(),
                JsonSerializer.Serialize(a, jsonOptions.Value.JsonSerializerOptions)))
            .ToList();
    }
}

file static class SelectEnumExtensions
{
    internal static string GetCustomName<T>(this T item) where T : struct, Enum
        => item switch
        {
            Region region => region switch
            {
                Region.NorthAmerica => "North America",
                Region.Europe => "Europe",
                Region.Australia => "Australia",
            },
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
            _ => item.GetEnumMemberName() 
        };

    internal static string GetEnumMemberName<T>(this T value)
        where T : struct, Enum =>
        typeof(T)
            .GetProperty(value.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>()
            ?.Value
        ?? value.ToString();
    
}