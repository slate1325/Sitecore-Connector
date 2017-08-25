﻿using System.Collections.Generic;
using AgencyOasis.MediaFramework.Brightcove.Json.Converters;
using Newtonsoft.Json;
using System;

namespace AgencyOasis.MediaFramework.Brightcove.Entities
{
    public class PlayList : AgencyOasis.MediaFramework.Brightcove.Entities.Asset
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "video_ids")]
        public List<string> VideoIds { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type")]
        public string PlaylistType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "search")]
        [JsonConverter(typeof(BrightcovePlaylistSearchFieldConverter))]
        public PlayListSearch PlayListSearch { get; set; }

        [JsonProperty("favorite", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Favorite { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreationDate { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastModifiedDate { get; set; }
    }
}