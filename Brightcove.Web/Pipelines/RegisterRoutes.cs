using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace Brightcove.Web.Pipelines
{
    public class RegisterRoutes
    {
        public void Process(PipelineArgs args)
        {
            //Register controllers as part of the shell site so we can limit requests to authenticated users
            RouteTable.Routes.MapRoute("Brightcove", "sitecore/shell/brightcove/{controller}/{action}", new
            {
                action = "Index"
            });
        }
    }
}