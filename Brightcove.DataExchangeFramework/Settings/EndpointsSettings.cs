using Sitecore.DataExchange;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Models;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;

namespace Brightcove.DataExchangeFramework.Settings
{
    public class BrightcoveEndpointSettings : IPlugin
    {
        public Endpoint BrightcoveEndpoint { get; set; }
        public Endpoint SitecoreEndpoint { get; set; }
    }
}