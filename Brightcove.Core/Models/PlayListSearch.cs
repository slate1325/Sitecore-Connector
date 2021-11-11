using System.Collections.Generic;
using Newtonsoft.Json;

namespace Brightcove.Core.Models
{
    public class PlayListSearch
    {
        public TagInclusion? TagInclusion { get; set; }

        public List<string> FilterTags { get; set; }
    }
}
