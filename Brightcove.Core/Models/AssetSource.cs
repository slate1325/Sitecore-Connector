using Newtonsoft.Json;

namespace Brightcove.Core.Models
{
    public class AssetSource
    {
        [JsonProperty("src", NullValueHandling = NullValueHandling.Ignore)]
        public string Src { get; set; }
    }
}
