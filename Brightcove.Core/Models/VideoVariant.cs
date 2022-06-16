using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Brightcove.Core.Models
{
    /// <summary>
    /// Represents a video object from the Brightcove API
    /// For more information, see http://support.brightcove.com/en/video-cloud/docs/media-api-objects-reference#Video
    /// </summary>
    public class VideoVariant
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("long_description", NullValueHandling = NullValueHandling.Ignore)]
        public string LongDescription { get; set; }

        [JsonProperty("custom_fields", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> CustomFields { get; set; }

        public VideoVariant ShallowCopy()
        {
            return (VideoVariant)this.MemberwiseClone();
        }
    }
}
