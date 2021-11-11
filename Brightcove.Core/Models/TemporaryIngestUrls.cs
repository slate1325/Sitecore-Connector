using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightcove.Core.Models
{
    public class TemporaryIngestUrls
    {
        [JsonProperty("bucket", NullValueHandling = NullValueHandling.Ignore)]
        public string Bucket { get; set; }

        [JsonProperty("object_key", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectKey { get; set; }

        [JsonProperty("access_key_id", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessKeyId { get; set; }

        [JsonProperty("secret_access_key", NullValueHandling = NullValueHandling.Ignore)]
        public string SecretAccessKey { get; set; }

        [JsonProperty("session_token", NullValueHandling = NullValueHandling.Ignore)]
        public string SessionToken { get; set; }

        [JsonProperty("signed_url", NullValueHandling = NullValueHandling.Ignore)]
        public string SignedUrl { get; set; }

        [JsonProperty("api_request_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiRequestUrl { get; set; }

        [JsonProperty("video_id", NullValueHandling = NullValueHandling.Ignore)]
        public string VideoId { get; set; }
    }
}
