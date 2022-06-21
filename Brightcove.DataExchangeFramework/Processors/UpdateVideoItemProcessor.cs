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
using Brightcove.DataExchangeFramework.Helpers;
using Sitecore.DataExchange.Providers.Sc.Plugins;
using Sitecore.Data;

namespace Brightcove.DataExchangeFramework.Processors
{
    public class UpdateVideoItemProcessor : BasePipelineStepProcessor
    {
        protected override void ProcessPipelineStepInternal(PipelineStep pipelineStep = null, PipelineContext pipelineContext = null, ILogger logger = null)
        {
            var mappingSettings = GetPluginOrFail<MappingSettings>();
            var endpointSettings = GetPluginOrFail<EndpointSettings>();
            var itemModelRepository = GetPluginOrFail<ItemModelRepositorySettings>(endpointSettings.EndpointTo).ItemModelRepository;

            ItemModel item = null;
            Video model = null;

            try
            {
                item = (ItemModel)this.GetObjectFromPipelineContext(mappingSettings.TargetObjectLocation, pipelineContext, logger);
                model = (Video)this.GetObjectFromPipelineContext(mappingSettings.SourceObjectLocation, pipelineContext, logger);

                foreach (IMappingSet mappingSet in mappingSettings.MappingSets)
                {
                    var mappingContext = Mapper.ApplyMapping(mappingSet, model, item);

                    if (Mapper.HasErrors(mappingContext))
                    {
                        LogError($"Failed to apply mapping to the item '{item.GetItemId()}': {Mapper.GetFailedMappings(mappingContext)}");
                    }
                    else
                    {
                        LogDebug($"Applied mapping to the item '{item.GetItemId()}'");
                    }
                }

                if (!ItemUpdater.Update(itemModelRepository, item))
                {
                    LogError($"Failed to update the item '{item.GetItemId()}'");
                }
                else
                {
                    LogDebug($"Updated the item '{item.GetItemId()}'");
                }
            }
            catch(Exception ex)
            {
                LogError($"An unexpected error occured updating the item '{item?.GetItemId()}'", ex);
            }
        }

        /*
        private void UpdateVariants(ItemModel item, Video video)
        {
            //var itemm = Sitecore.Context.ContentDatabase.GetItem(new ID(item.GetItemId()));
            BrightcoveService service = null;
            var variants = service.GetVideoVariants(video.Id);

            if(variants.Any())
            {

            }
        }
        */
    }
}
