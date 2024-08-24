using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoSession1_WebAPI.Converters
{
	public class DateTimeConverter : JsonConverter<DateTime>
	{
		private string formatDate = "dd/MM/yyyy HH:mm:ss";
		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return DateTime.ParseExact(reader.GetString(), formatDate, CultureInfo.InvariantCulture);
		}

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString(formatDate));

		}
	}
}
