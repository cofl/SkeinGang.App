using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SkeinGang.Data.Enums;

public enum Region
{
    [JsonStringEnumMemberName("na")]
    [EnumMember(Value = "na")]
    NorthAmerica,

    [JsonStringEnumMemberName("eu")]
    [EnumMember(Value = "eu")]
    Europe,

    [JsonStringEnumMemberName("au")]
    [EnumMember(Value = "au")]
    Australia,
}
