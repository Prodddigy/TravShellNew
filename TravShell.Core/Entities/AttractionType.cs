using System.Text.Json.Serialization;

namespace TravShell.Core.Entities
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttractionType
    {

        Accommodation = 0,
        Attraction = 1,
        Restaurant = 2,
    }
}