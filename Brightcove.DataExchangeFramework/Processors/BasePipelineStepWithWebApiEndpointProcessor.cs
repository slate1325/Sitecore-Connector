using Brightcove.Core.Models;
using Brightcove.Core.Services;
using Brightcove.DataExchangeFramework.Settings;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Extensions;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightcove.DataExchangeFramework.Processors
{
    public class BasePipelineStepWithWebApiEndpointProcessor : BasePipelineStepWithEndpointFromProcessor
    {
        protected WebApiSettings WebApiSettings { get; set; }

        protected override void ProcessPipelineStep(PipelineStep pipelineStep = null, PipelineContext pipelineContext = null, ILogger logger = null)
        {
            base.ProcessPipelineStep(pipelineStep, pipelineContext, logger);

            if(pipelineContext.CriticalError)
            {
                return;
            }

            WebApiSettings = EndpointFrom.GetPlugin<WebApiSettings>();

            if (WebApiSettings == null)
            {
                logger.Error(
                    "No web api settings specified on the endpoint. " +
                    "(pipeline step: {0})",
                    pipelineStep.Name);

                pipelineContext.CriticalError = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(WebApiSettings.AccountId))
            {
                logger.Error(
                    "No account ID is specified on the endpoint. " +
                    "(pipeline step: {0})",
                    pipelineStep.Name);

                pipelineContext.CriticalError = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(WebApiSettings.ClientId))
            {
                logger.Error(
                    "No client ID is specified on the endpoint. " +
                    "(pipeline step: {0})",
                    pipelineStep.Name);

                pipelineContext.CriticalError = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(WebApiSettings.ClientSecret))
            {
                logger.Error(
                    "No client secret is specified on the endpoint. " +
                    "(pipeline step: {0})",
                    pipelineStep.Name);

                pipelineContext.CriticalError = true;
                return;
            }
        }
    }
}
