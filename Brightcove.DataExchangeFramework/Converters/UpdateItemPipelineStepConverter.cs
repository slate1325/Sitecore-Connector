using Brightcove.DataExchangeFramework.Settings;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Providers.Sc.Converters.PipelineSteps;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;

namespace Brightcove.DataExchangeFramework.Converters
{
    [SupportedIds("{F598D123-2FE9-45D3-99E2-3E4B5063190A}")]
    public class UpdateItemPipelineStepConverter : BasePipelineStepConverter
    {
        public UpdateItemPipelineStepConverter(IItemModelRepository repository) : base(repository) { }

        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            MappingSettings mappingSettings = new MappingSettings()
            {
                MappingSets = this.ConvertReferencesToModels<IMappingSet>(source, "MappingSet"),
                //MappingsAppliedActions = this.ConvertReferencesToModels<IMappingsAppliedAction>(source, "Actions"),
                SourceObjectLocation = this.GetGuidValue(source, "SourceObjectLocation"),
                TargetObjectLocation = this.GetGuidValue(source, "TargetObjectLocation")
            };

            pipelineStep.AddPlugin<MappingSettings>(mappingSettings);

            EndpointSettings endpointSettings = new EndpointSettings()
            {
                EndpointTo = this.ConvertReferenceToModel<Endpoint>(source, "EndpointTo")
            };

            pipelineStep.AddPlugin<EndpointSettings>(endpointSettings);
        }
    }
}