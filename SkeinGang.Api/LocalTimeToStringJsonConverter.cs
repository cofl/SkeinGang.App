using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using NodaTime;

namespace SkeinGang.Api;

public class LocalTimeToStringJsonConverter: JsonConverter<LocalTime>
{
    public override LocalTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is not { } value)
            throw new ArgumentNullException(nameof(reader), "Cannot read null into LocalTime.");
        if (!TimeOnly.TryParseExact(value, "HH:mms:ss", out var time))
            throw new ArgumentException("Invalid time format.");
        return LocalTime.FromTimeOnly(time);
    }

    public override void Write(Utf8JsonWriter writer, LocalTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
    }
}
