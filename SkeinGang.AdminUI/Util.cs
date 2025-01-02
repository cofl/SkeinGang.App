using System.Runtime.CompilerServices;

namespace SkeinGang.AdminUI;

internal static class Assert
{
    internal static void MustBeNull<T>(T? value, [CallerArgumentExpression(nameof(value))] string? member = null)
    {
        if (value is not null)
            throw new ArgumentNullException(member, "Value must be null.");
    }

    internal static void MustNotBeNull<T>(T? value, [CallerArgumentExpression(nameof(value))] string? member = null)
    {
        if (value is null)
            throw new ArgumentNullException(member, "Value cannot be null.");
    }
}
