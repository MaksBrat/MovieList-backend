using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MovieList.Core.Utility.Converters
{
    public class FloatRoundingConverter : JsonConverter<float>
    {
        public override void WriteJson(JsonWriter writer, float value, JsonSerializer serializer)
        {
            JToken.FromObject(value).WriteTo(writer);
        }

        public override float ReadJson(JsonReader reader, Type objectType, float existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            float floatValue = token.ToObject<float>();

            return (float)Math.Round(floatValue, 2);
        }
    }
}
