using Sitecore.DataExchange;
using Sitecore.DataExchange.DataAccess;
using System;

namespace Brightcove.DataExchangeFramework.Settings
{
    public class ResolveAssetItemSettings : IPlugin
    {
        public string AcccountItemId { get; set; } = "";

        public string RelativePath { get; set; } = "";
    }
}