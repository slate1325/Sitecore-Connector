using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Brightcove.Core.Models
{
    /// <summary>
    /// Represents a players object from the Brightcove API
    /// For more information, see https://apis.support.brightcove.com/player-management/references/reference.html#operation/GetPlayer
    /// </summary>
    public class Player : Asset
    {
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreattionDate { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("branches", NullValueHandling = NullValueHandling.Ignore)]
        public Branches Branches { get; set; }

        public Player ShallowCopy()
        {
            return (Player)this.MemberwiseClone();
        }
    }

    public class Branches
    {
        [JsonProperty("master", NullValueHandling = NullValueHandling.Ignore)]
        public Master Master { get; set; }
    }

    public class Master
    {
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedAt { get; set; }
    }

    public class PlayerList
    {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Player> Items { get; set; }
    }


}
