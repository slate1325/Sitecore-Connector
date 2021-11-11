namespace Sitecore.MediaFramework.Pipelines.MediaGenerateMarkup
{
  using System.Collections.Generic;

  public class PlayerMarkupResult
  {
    public PlayerMarkupResult()
    {
      this.Html = string.Empty;
      this.CssUrls = new List<string>();
      this.ScriptUrls = new List<string>();
      this.BottomScripts = new Dictionary<string, string>();
    }

    public string Html { get; set; }
    public List<string> CssUrls { get; set; }
    public List<string> ScriptUrls { get; set; }

    public Dictionary<string, string> BottomScripts { get; set; } 
  }
}