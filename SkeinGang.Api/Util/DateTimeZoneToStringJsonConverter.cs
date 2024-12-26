using System.Text.Json;
using System.Text.Json.Serialization;
using NodaTime;

namespace SkeinGang.Api.Util;

internal class DateTimeZoneToStringJsonConverter: JsonConverter<DateTimeZone>
{
    public override DateTimeZone? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is not {} id)
            return null;
        if (DateTimeZoneProviders.Tzdb.GetZoneOrNull(id) is { } zone)
            return zone;
        #pragma warning disable CA2208
        throw new ArgumentOutOfRangeException(nameof(id), id, "Id is not a known time zone.");
        #pragma warning restore CA2208
    }

    public override void Write(Utf8JsonWriter writer, DateTimeZone value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
