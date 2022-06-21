using Sitecore.DataExchange;
using Sitecore.DataExchange.DataAccess;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;

namespace Brightcove.DataExchangeFramework.Settings
{
    public class MappingSettings : IPlugin
    {
        public IEnumerable<IMappingSet> MappingSets { get; set; }

        //public IEnumerable<IMappingsAppliedAction> MappingsAppliedActions { get; set; }

        public Guid SourceObjectLocation { get; set; }

        public Guid TargetObjectLocation { get; set; }
    }
}