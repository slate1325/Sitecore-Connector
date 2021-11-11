
using Newtonsoft.Json;
using System;

namespace Brightcove.Core.Models
{
  public class Asset
  {
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string Id { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("reference_id", NullValueHandling = NullValueHandling.Ignore)]
    public string ReferenceId { get; set; }

    [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
    public ImageAssets Images { get; set; }

    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string ShortDescription { get; set; }

    [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime? LastModifiedDate { get; set; }

    [JsonIgnore]
    public DateTime LastSyncTime { get; set; }

    public override string ToString()
    {
      return string.Format("(type:{0},id:{1},name:{2})", (object) this.GetType().Name, (object) this.Id, (object) this.Name);
    }
  }
}
