using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SkeinGang.Data.Enums;

public enum ContentDifficulty
{
    [JsonStringEnumMemberName("normal")]
    [EnumMember(Value = "normal")]
    NormalMode,
    
    [JsonStringEnumMemberName("challenge")]
    [EnumMember(Value = "challenge")]
    ChallengeMode,
    
    [JsonStringEnumMemberName("legendary")]
    [EnumMember(Value = "legendary")]
    LegendaryMode,
}