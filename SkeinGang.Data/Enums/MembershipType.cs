using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SkeinGang.Data.Enums;

public enum MembershipType
{
    [JsonStringEnumMemberName("member")]
    [EnumMember(Value = "member")]
    Member,

    [JsonStringEnumMemberName("static_rep")]
    [EnumMember(Value = "static_rep")]
    StaticRep,

    [JsonStringEnumMemberName("honorary")]
    [EnumMember(Value = "honorary")]
    Honorary,
}
