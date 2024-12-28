using System.Text.Json.Serialization;

namespace SkeinGang.Api.Util;

/// <summary>
/// A boolean value which may be true, false, or either.
/// </summary>
public enum AnyBoolean
{
    /// <summary>
    /// True.
    /// </summary>
    [JsonPropertyName("true")]
    True,
    
    /// <summary>
    /// False.
    /// </summary>
    [JsonPropertyName("false")]
    False,
    
    /// <summary>
    /// Either true or false. 
    /// </summary>
    [JsonPropertyName("any")]
    Any
}