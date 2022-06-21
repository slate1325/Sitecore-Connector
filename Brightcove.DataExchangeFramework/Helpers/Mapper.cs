using Brightcove.Core.Exceptions;
using Brightcove.Core.Models;
using Brightcove.Core.Services;
using Brightcove.DataExchangeFramework.Settings;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Extensions;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Diagnostics;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Brightcove.Core.Extensions;
using Sitecore.DataExchange.ApplyMapping;

namespace Brightcove.DataExchangeFramework.Helpers
{
    public static class Mapper
    {
        public static MappingContext ApplyMapping(IMappingSet mappingSet, object source, object target)
        {
            if (mappingSet == null)
            {
                throw new ArgumentNullException(nameof(mappingSet));
            }

            if (mappingSet.Mappings == null)
            {
                throw new ArgumentNullException(nameof(mappingSet.Mappings));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            MappingContext mappingContext = new MappingContext()
            {
                Source = source,
                Target = target
            };

            mappingSet.Run(mappingContext);

            //pipelineContext.GetSynchronizationSettings().IsTargetDirty = this.IsTargetDirty(mappingContext, mappingSettings, pipelineContext, logger);
            //if (!this.ShouldRunMappingsAppliedActions(mappingContext, mappingSettings, pipelineContext, logger) return;
            //this.RunMappingsAppliedActions(mappingContext, mappingSettings, pipelineContext, logger);

            return mappingContext;
        }

        public static string GetFailedMappings(MappingContext mappingContext)
        {
            return $"Failed mapping(s) {mappingContext.RunFail.Count}: '{string.Join(",", mappingContext.RunFail.Select(m => m.Identifier).ToArray())}'";
        }

        public static bool HasErrors(MappingContext mappingContext)
        {
            return mappingContext.RunFail.Any();
        }
    }
}
