namespace Sitecore.MediaFramework.Mvc.Extentions
{
  using System.Collections.Generic;
  using System.Web;
  using System.Web.Mvc;

  using Sitecore.Configuration;
  using Sitecore.Diagnostics;
  using Sitecore.MediaFramework.Mvc.Data;
  using Sitecore.MediaFramework.Mvc.Helpers;
  using Sitecore.Mvc.Presentation;

  public static class HtmlResourcesHelperExtentions
  {
    public static readonly IHtmlResourcesHelper Helper = new HtmlResourcesHelper();

    public static HtmlResourcesContext CreateResourcesContext(this HtmlHelper htmlHelper, string contextKey, HtmlResources resources = null)
    {
      Assert.ArgumentNotNull(htmlHelper, "htmlHelper");

      return Helper.CreateResourcesContext(htmlHelper.ViewContext.HttpContext, contextKey, resources);
    }

    public static HtmlResourcesContext CreateResourcesContext(this HttpContextBase httpContext, string contextKey, HtmlResources resources = null)
    {
      return Helper.CreateResourcesContext(httpContext, contextKey, resources);
    }

    public static Stack<HtmlResourcesContext> GetHtmlResources(this HtmlHelper htmlHelper, string contextKey)
    {
      Assert.ArgumentNotNull(htmlHelper, "htmlHelper");

      return Helper.GetHtmlResources(htmlHelper.ViewContext.HttpContext, contextKey);
    }

    public static Stack<HtmlResourcesContext> GetHtmlResources(this HttpContextBase httpContext, string contextKey)
    {
      return Helper.GetHtmlResources(httpContext, contextKey);
    }

    public static IHtmlString Render(this Stack<HtmlResourcesContext> resources)
    {
      return Helper.Render(resources);
    }

    public static IHtmlString RenderItemId(this HtmlHelper htmlHelper)
    {
      return Helper.RenderItemId(PageContext.CurrentOrNull);
    }

    public static IHtmlString RenderItemId(this PageContext pageContext)
    {
      return Helper.RenderItemId(pageContext);
    }

        public static IHtmlString RenderBrightcoveHeader(this HtmlHelper htmlHelper)
        {
            string header = "";
            header += "<link href='/sitecore modules/Web/Brightcove/css/brightcove.css' rel='stylesheet'>\n";
            header += "<script type='text/javascript' src='/sitecore/shell/Controls/Lib/jQuery/jquery.js'></script>\n";
            header += "<script type='text/javascript' src='/sitecore modules/Web/Brightcove/js/fancybox/jquery.fancybox-1.3.4.pack.js'></script>\n";
            header += "<script type='text/javascript' src='/sitecore modules/Web/Brightcove/js/brightcove.js'></script>\n";
            header += "<script type='text/javascript' src='/sitecore modules/Web/Brightcove/js/Analytics/PlayerEventsListener.js'></script>\n";
            header += "<script type='text/javascript' src='/sitecore modules/Web/Brightcove/js/Analytics/brightcove.js'></script>\n";
            return new HtmlString(header);
        }

        public static IHtmlString RenderBrightcoveScripts(this HtmlHelper htmlHelper)
        {
            return htmlHelper.GetHtmlResources("SMF_RESOURCES_BODY").Render();
        }

        /*
    public static void RegisterDefaultResources(this HtmlHelper htmlHelper)
    {
      Helper.RegisterDefaultResources(htmlHelper.ViewContext.HttpContext);
    }

    public static void RegisterDefaultResources(this HttpContextBase httpContext)
    {
      Helper.RegisterDefaultResources(httpContext);
    }
        */
    }
}