namespace Sitecore.MediaFramework.Utils
{
  using System.Linq;

  using Sitecore.Data.Items;

  public class MediaItemUtil
  {
    public static bool IsMediaElement(TemplateItem template)
    {
      if (template.ID == Brightcove.MediaFramework.Brightcove.TemplateIDs.MediaElement)
      {
        return true;
      }

      return template.BaseTemplates.Any(IsMediaElement);
    }
  }
}