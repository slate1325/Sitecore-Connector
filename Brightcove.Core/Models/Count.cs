using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightcove.Core.Models
{
    public class Count
    {
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int Value { get; set; }
    }
}
