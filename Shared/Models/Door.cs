using Newtonsoft.Json;

namespace Shared
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Door
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        //TODO maybe an Enum with Door States could be relevant here
        [JsonProperty(PropertyName = "isClosed")]
        public bool IsClosed { get; set; }
        
        [JsonProperty(PropertyName = "isLocked")]
        public bool IsLocked { get; set; }
    }
}
