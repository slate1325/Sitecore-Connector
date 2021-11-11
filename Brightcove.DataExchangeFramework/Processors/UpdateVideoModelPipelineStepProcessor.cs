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

namespace Brightcove.DataExchangeFramework.Processors
{
    public class UpdateVideoModelPipelineStepProcessor : BasePipelineStepWithWebApiEndpointProcessor
    {
        BrightcoveService service;

        protected override void ProcessPipelineStep(PipelineStep pipelineStep = null, PipelineContext pipelineContext = null, ILogger logger = null)
        {
            base.ProcessPipelineStep(pipelineStep, pipelineContext, logger);

            if (pipelineContext.CriticalError)
            {
                return;
            }

            var resolveAssetModelSettings = pipelineStep.GetPlugin<ResolveAssetModelSettings>();
            if (resolveAssetModelSettings == null)
            {
                logger.Error(
                    "No resolve asset model settings are specified for the pipeline step. " +
                    "(pipeline step: {0})",
                    pipelineStep.Name);

                pipelineContext.CriticalError = true;
                return;
            }

            try
            {
                service = new BrightcoveService(WebApiSettings.AccountId, WebApiSettings.ClientId, WebApiSettings.ClientSecret);

                Video video = (Video)pipelineContext.GetObjectFromPipelineContext(resolveAssetModelSettings.AssetModelLocation);
                ItemModel itemModel = (ItemModel)pipelineContext.GetObjectFromPipelineContext(resolveAssetModelSettings.AssetItemLocation);
                Item item = Sitecore.Context.ContentDatabase.GetItem(itemModel.GetItemId().ToString());

                //The item has been marked for deletion in Sitecore
                if ((string)itemModel["Delete"] == "1")
                {
                    logger.Info($"Deleting the brightcove model '{video.Id}' because it has been marked for deletion in Sitecore (pipeline step: {pipelineStep.Name})");
                    service.DeleteVideo(video.Id);

                    logger.Info($"Deleting the brightcove item '{item.ID}' because it has been marked for deleteion in Sitecore '{itemModel.GetItemId()}' (pipeline step: {pipelineStep.Name})");
                    item.Delete();

                    return;
                }

                DateTime lastSyncTime = DateTime.Parse(item["LastSyncTime"]);
                DateField lastModifiedTime = item.Fields["__Updated"];

                //If the brightcove item has been modified since the last sync then send the updates to brightcove
                //Unless the brightcove model has already been modified since the last sync (presumably outside of Sitecore)
                if (lastModifiedTime.DateTime > lastSyncTime)
                {
                    if (video.LastModifiedDate < lastSyncTime)
                    {
                        try
                        {
                            service.UpdateVideo(video);
                        }
                        //This is hacky fix to silent ignore invalid custom fields
                        //This should be removed when a more permant solution is found
                        catch(HttpStatusException ex)
                        {
                            if ((int)ex.Response.StatusCode != 422)
                                throw ex;

                            string message = ex.Response.Content.ReadAsString();

                            if (!message.Contains("custom_fields"))
                                throw ex;

                            //Rerun with the invalid custom fields removed so the rest of the updates are made
                            video.CustomFields = null;
                            this.ProcessPipelineStep(pipelineStep, pipelineContext, logger);
                            return;
                        }

                        logger.Debug($"Successfully updated the brightcove model '{video.Id}' (pipeline step: {pipelineStep.Name})");
                    }
                    else
                    {
                        logger.Warn($"Ignored changes made to brightcove item '{item.ID}' because the brightcove model '{video.Id}' has been modified since last sync. Please run the pull pipeline to get the latest changes (pipeline step: {pipelineStep.Name})");
                    }
                }
                else
                {
                    logger.Debug($"Ignored the brightcove item '{item.ID}' because it has not been updated since last sync (pipeline step: {pipelineStep.Name})");
                }
            }
            catch(Exception ex)
            {
                logger.Error($"Failed to update the brightcove model because an unexpected error occured (pipeline step: {pipelineStep.Name}, exception: {ex.Message})");
            }
        }
    }
}
