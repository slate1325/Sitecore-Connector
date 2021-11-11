using Sitecore.DataExchange;
using Sitecore.DataExchange.DataAccess;
using System;

namespace Brightcove.DataExchangeFramework.Settings
{
    public class ResolveAssetModelSettings : IPlugin
    {
        public Guid AssetItemLocation { get; set; }
        public Guid AssetModelLocation { get; set; }
    }
}