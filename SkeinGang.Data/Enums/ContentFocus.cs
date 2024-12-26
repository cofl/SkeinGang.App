using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SkeinGang.Data.Enums;

public enum ContentFocus
{
    [JsonStringEnumMemberName("hot")]
    [EnumMember(Value = "hot")]
    HeartOfThorns,
    
    [JsonStringEnumMemberName("pof")]
    [EnumMember(Value = "pof")]
    PathOfFire,
    
    [JsonStringEnumMemberName("hot_pof")]
    [EnumMember(Value = "hot_pof")]
    HeartOfThornsAndPathOfFire,
    
    [JsonStringEnumMemberName("eod")]
    [EnumMember(Value = "eod")]
    EndOfDragons,
    
    [JsonStringEnumMemberName("fractals")]
    [EnumMember(Value = "fractals")]
    Fractals,
    
    [JsonStringEnumMemberName("harvest_temple")]
    [EnumMember(Value = "harvest_temple")]
    HarvestTemple,
    
    [JsonStringEnumMemberName("temple_of_febe")]
    [EnumMember(Value = "temple_of_febe")]
    TempleOfFebe,
    
    [JsonStringEnumMemberName("competitive")]
    [EnumMember(Value = "competitive")]
    Competitive,
    
    [JsonStringEnumMemberName("social")]
    [EnumMember(Value = "social")]
    Social,
    
    [JsonStringEnumMemberName("janthir_wilds")]
    [EnumMember(Value = "janthir_wilds")]
    JanthirWilds,
}
