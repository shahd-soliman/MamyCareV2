using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace MamyCare.Helpers
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string Format = "dd/MM/yyyy HH:mm";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (DateTime.TryParseExact(value, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            throw new JsonException($"Invalid date format. Expected format: {Format}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
