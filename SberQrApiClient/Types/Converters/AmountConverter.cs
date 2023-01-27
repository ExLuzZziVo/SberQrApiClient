#region

using System;
using Newtonsoft.Json;

#endregion

namespace SberQrApiClient.Types.Converters
{
    /// <summary>
    /// Конвертер для денежных величин. Платежный шлюз работает с int
    /// </summary>
    public class AmountConverter : JsonConverter<decimal?>
    {
        public override void WriteJson(JsonWriter writer, decimal? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue((int)(Math.Round(value.Value, 2, MidpointRounding.AwayFromZero) * 100));
            }
        }

        public override decimal? ReadJson(JsonReader reader, Type objectType, decimal? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var value = (long?)reader.Value;

            return value == null ? null : (decimal?)value / 100;
        }
    }
}