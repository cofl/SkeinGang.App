using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace SkeinGang.Data.Context;

// ReSharper disable once UnusedType.Global
public class DateTimeZoneToStringConverter()
    : ValueConverter<DateTimeZone, string>(
        v => v.Id,
        v => GetDateTimeZone(v))
{
    private static DateTimeZone GetDateTimeZone(string timeZoneId) =>
        DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZoneId) ??
        throw new ArgumentOutOfRangeException(nameof(timeZoneId), timeZoneId, "Unknown time zone Id.");
}
