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
    public class BasePipelineStepWithEndpointFromProcessor : BasePipelineStepWithEndpointsProcessor
    {
        protected Endpoint EndpointFrom { get; set; }


        protected override void ProcessPipelineStep(PipelineStep pipelineStep = null, PipelineContext pipelineContext = null, ILogger logger = null)
        {
            if (pipelineStep == null)
            {
                throw new ArgumentNullException(nameof(pipelineStep));
            }
            if (pipelineContext == null)
            {
                throw new ArgumentNullException(nameof(pipelineContext));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            EndpointSettings endpointSettings = pipelineStep.GetEndpointSettings();

            if (endpointSettings == null)
            {
                this.Log(new Action<string>(logger.Error), pipelineContext, "Pipeline step processing will abort because the pipeline step is missing a plugin.", new string[1]
                {
                    "plugin: " + typeof (EndpointSettings).FullName
                });

                pipelineContext.CriticalError = true;
                return;
            }

            EndpointFrom = endpointSettings.EndpointFrom;

            if (EndpointFrom == null)
            {
                this.Log(new Action<string>(logger.Error), pipelineContext, "Pipeline step processing will abort because the pipeline step is missing an endpoint to read from.", new string[2]
                {
                    "plugin: " + typeof (EndpointSettings).FullName,
                    "property: EndpointFrom"
                });

                pipelineContext.CriticalError = true;
                return;
            }
            else if (!this.IsEndpointValid(EndpointFrom, pipelineStep, pipelineContext, logger))
            {
                this.Log(new Action<string>(logger.Error), pipelineContext, "Pipeline step processing will abort because the endpoint to read from is not valid.", new string[1]
                {
                    "endpoint: " + EndpointFrom.Name
                });

                pipelineContext.CriticalError = true;
                return;
            }
        }
    }
}
