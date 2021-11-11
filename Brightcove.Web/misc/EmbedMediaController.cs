namespace Sitecore.MediaFramework.Mvc.Controllers
{
  using System.Linq;
  using System.Web.Mvc;
    using Brightcove.MediaFramework.Brightcove.Players;
    using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.MediaFramework.Pipelines.MediaGenerateMarkup;
  using Sitecore.MediaFramework.Players;
  using Sitecore.Mvc.Presentation;

  public class EmbedMediaController : Controller
  {
        private string renderVideoViewPath = "~/sitecore modules/Web/Brightcove/Views/EmbedMedia.cshtml";

    public ActionResult RenderVideo()
    {
      Rendering rendering = RenderingContext.Current.Rendering;

      if (!ID.IsID(rendering.DataSource))
      {
        return this.View(this.renderVideoViewPath, new MediaGenerateMarkupArgs());
      }

      PlayerProperties properties = new PlayerProperties(rendering.Parameters.ToDictionary(p => p.Key, p => p.Value))
      {
        ItemId = new ID(rendering.DataSource)
      };

      //MediaGenerateMarkupPipeline.Run(args);

            var args = new MediaGenerateMarkupArgs
            {
                MarkupType = MarkupType.Html,
                Properties = properties,
                MediaItem = Sitecore.Context.Database.GetItem(properties.ItemId),
                PlayerItem = Sitecore.Context.Database.GetItem(properties.PlayerId)
            };

            args.Properties.Template = args.MediaItem.Template.ID;
            args.AccountItem = Sitecore.Context.Database.GetItem(string.Join("/", args.MediaItem.Paths.Path.Split('/').Take(5)));

            var generator = new BrightcovePlayerMarkupGenerator();
            args.Result = generator.Generate(args);

            return this.View(this.renderVideoViewPath, args);
    }
  }
}