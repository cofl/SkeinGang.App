using System.Text.RegularExpressions;

namespace SkeinGang.Data;

public static partial class Util
{
    /// <summary>
    /// A regular expression for matching <code>\p{Ll}</code> (Letter, lowercase)
    /// followed by <code>\p{Lu}</code> (Letter, uppercase).
    /// </summary>
    /// <remarks>Used for inserting <code>_</code> between words in a PascalCase or camelCase identifier.</remarks>
    /// <returns>A compiled regular expression singleton.</returns>
    [GeneratedRegex(@"\p{Ll}\p{Lu}")]
    private static partial Regex SnakeCaseRegex();

    /// <summary>
    /// Convert a string from PascalCase or camelCase to snake_case.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The converted string.</returns>
    public static string ToSnake(this string str) => SnakeCaseRegex().Replace(str, "$1_$2").ToLowerInvariant();
}