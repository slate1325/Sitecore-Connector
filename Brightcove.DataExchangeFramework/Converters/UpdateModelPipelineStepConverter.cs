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
    [SupportedIds("{D79A5F5C-9A5E-4B1C-B884-8E3B97CACA2D}")]
    public class UpdateModelPipelineStepConverter : BasePipelineStepConverter
    {
        public UpdateModelPipelineStepConverter(IItemModelRepository repository) : base(repository) { }

        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            MappingSettings mappingSettings = new MappingSettings()
            {
                ModelMappingSets = this.ConvertReferencesToModels<IMappingSet>(source, "ModelMappingSets"),
                VariantMappingSets = this.ConvertReferencesToModels<IMappingSet>(source, "VariantMappingSets"),
                SourceObjectLocation = this.GetGuidValue(source, "SourceObjectLocation"),
                TargetObjectLocation = this.GetGuidValue(source, "TargetObjectLocation")
            };

            pipelineStep.AddPlugin<MappingSettings>(mappingSettings);

            EndpointSettings endpointSettings = new EndpointSettings()
            {
                EndpointTo = this.ConvertReferenceToModel<Endpoint>(source, "EndpointTo"),
                EndpointFrom = this.ConvertReferenceToModel<Endpoint>(source, "EndpointFrom")
            };

            pipelineStep.AddPlugin<EndpointSettings>(endpointSettings);

            Guid endpointId = this.GetGuidValue(source, "EndpointTo");

            if(endpointId == null)
            {
                return;
            }

            ItemModel endpointModel = ItemModelRepository.Get(endpointId);

            if(endpointModel == null)
            {
                return;
            }

            Guid accountItemId = this.GetGuidValue(endpointModel, "Account");

            if(accountItemId == null)
            {
                return;
            }

            Item accountItem = Sitecore.Context.ContentDatabase.GetItem(new ID(accountItemId));

            if(accountItem == null)
            {
                return;
            }

            WebApiSettings webApiSettings = new WebApiSettings();

            if (accountItem != null)
            {
                webApiSettings.AccountId = accountItem["AccountId"];
                webApiSettings.ClientId = accountItem["ClientId"];
                webApiSettings.ClientSecret = accountItem["ClientSecret"];
            }

            endpointSettings.EndpointTo.AddPlugin(webApiSettings);
        }
    }
}