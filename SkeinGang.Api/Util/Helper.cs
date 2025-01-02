using System.Collections;

namespace SkeinGang.Api.Util;

internal static class Helper
{
    internal static List<T> ListOfNotNull<T>(params T?[] items) => items.OfType<T>().ToList();

    internal static TResult? IfNotEmpty<T, TResult>(this T collection, Func<T, TResult> fn)
        where T : ICollection
        where TResult : notnull
        => collection.Count > 0 ? fn(collection) : default;
}
