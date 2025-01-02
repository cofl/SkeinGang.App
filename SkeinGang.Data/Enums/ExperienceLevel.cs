using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SkeinGang.Data.Enums;

public enum ExperienceLevel
{
    [JsonStringEnumMemberName("progression")]
    [EnumMember(Value = "progression")]
    Progression,

    [JsonStringEnumMemberName("experienced")]
    [EnumMember(Value = "experienced")]
    Experienced,
}
