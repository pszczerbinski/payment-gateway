namespace PaymentGateway.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Defines a <see cref="Currency"/> JSON converter.
    /// </summary>
    public class JsonCurrencyConverter : JsonConverter<Currency>
    {
        public override Currency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Currency.FromCode(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Currency value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Code);
        }
    }
}
