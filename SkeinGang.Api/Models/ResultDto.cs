using SkeinGang.Api.Util;

namespace SkeinGang.Api.Models;

/// <summary>
///     A list of results.
/// </summary>
/// <param name="HasMore">If more results can be requested by adjusting the limit/offset parameters of the query.</param>
/// <param name="Data">The list of results.</param>
/// <typeparam name="T">The type of each result.</typeparam>
[DefaultRequired]
public record ResultDto<T>(bool HasMore, List<T> Data);
