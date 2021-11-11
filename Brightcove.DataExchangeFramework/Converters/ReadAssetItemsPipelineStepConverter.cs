using Brightcove.DataExchangeFramework.Settings;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Providers.Sc.Converters.PipelineSteps;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightcove.DataExchangeFramework.Converters
{
    [SupportedIds("{51EF874F-CCA2-402D-8D5F-289E635D68E3}")]
    public class ReadAssetItemsPipelineStepConverter : ReadSitecoreItemsStepConverter
    {
        public ReadAssetItemsPipelineStepConverter(IItemModelRepository repository) : base(repository)
        {

        }

        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            base.AddPlugins(source, pipelineStep);

            Item endpointItem = Sitecore.Context.ContentDatabase.GetItem(new ID(this.GetGuidValue(source, "BrightcoveEndpoint")));

            ResolveAssetItemSettings resolveAssetItemSettings = new ResolveAssetItemSettings()
            {
                AcccountItemId = endpointItem["Account"],
                RelativePath = this.GetStringValue(source, "RelativePath")
            };

            pipelineStep.AddPlugin<ResolveAssetItemSettings>(resolveAssetItemSettings);
        }
    }
}
