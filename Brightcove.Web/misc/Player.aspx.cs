namespace Sitecore.MediaFramework.UI.Sublayouts
{
  using System;
    using System.Linq;
    using System.Web.UI;
    using Brightcove.MediaFramework.Brightcove;
    using Brightcove.MediaFramework.Brightcove.Players;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.MediaFramework.Pipelines.MediaGenerateMarkup;
  using Sitecore.MediaFramework.Players;
    using Sitecore.Shell.Framework.Commands;

    public partial class Player : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }

            PlayerProperties properties = new PlayerProperties(this.Request.QueryString);
            properties.Collection.Add(BrightcovePlayerParameters.EmbedStyle, "true");

            properties.Width = MediaFrameworkContext.PreviewSize.Width;
            properties.Height = MediaFrameworkContext.PreviewSize.Height;


            var itemId = new ID(Guid.Parse(this.Request.QueryString["itemId"]));
            var playerId = new ID(Guid.Parse(this.Request.QueryString["playerId"]));

            var args = new MediaGenerateMarkupArgs
            {
                MarkupType = MarkupType.Frame,
                Properties = properties,
                MediaItem = Sitecore.Data.Database.GetDatabase("master").GetItem(itemId),
                PlayerItem = Sitecore.Data.Database.GetDatabase("master").GetItem(playerId)
            };

            args.Properties.Template = args.MediaItem.Template.ID;
            args.AccountItem = Sitecore.Context.ContentDatabase.GetItem(string.Join("/", args.MediaItem.Paths.Path.Split('/').Take(5)));

            var generator = new BrightcovePlayerMarkupGenerator();
            var result = generator.Generate(args);

            //PlayerManager.RegisterDefaultResources(this.Page);

            //if (!args.Aborted)
            //{
            this.PlayerContainer.InnerHtml = result.Html;
            this.PlayerContainer.Attributes["data-mf-params"] = properties.ToString();

            //PlayerManager.RegisterResources(this.Page, args.Result);
            //}
            //else
            //{
            //  this.PlayerContainer.InnerHtml = PlayerManager.GetEmptyValue();
            //}
        }
    }
}