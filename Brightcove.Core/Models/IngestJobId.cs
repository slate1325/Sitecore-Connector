using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightcove.Core.Models
{
    public class IngestJobId
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string JobId { get; set; }
    }
}
