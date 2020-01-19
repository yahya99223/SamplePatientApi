using System.Text.Json.Serialization;

namespace Shared.Models.enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum IsVip { Yes, No }
}
