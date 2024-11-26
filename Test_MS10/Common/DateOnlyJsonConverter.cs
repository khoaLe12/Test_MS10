using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient.Server;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Test_MS10.Common;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected string for DateOnly value.");
        }

        var value = reader.GetString();

        if (DateOnly.TryParseExact(value, Format, null, System.Globalization.DateTimeStyles.None, out var dateOnly))
        {
            return dateOnly;
        }

        throw new JsonException($"Invalid date format. Expected format is {Format}.");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}
