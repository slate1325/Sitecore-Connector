﻿using Newtonsoft.Json;

namespace AgencyOasis.MediaFramework.Brightcove.Entities
{
    public class ImageAssets
    {
        [JsonProperty("poster", NullValueHandling = NullValueHandling.Ignore)]
        public ImageAsset Poster { get; set; }

        [JsonProperty("thumbnail", NullValueHandling = NullValueHandling.Ignore)]
        public ImageAsset Thumbnail { get; set; }
    }
}
