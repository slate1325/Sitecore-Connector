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

namespace Brightcove.DataExchangeFramework.Processors
{
    public class UpdateVideoModelPipelineStepProcessor : BasePipelineStepProcessor
    {
        BrightcoveService service;

        protected override void ProcessPipelineStepInternal(PipelineStep pipelineStep = null, PipelineContext pipelineContext = null, ILogger logger = null)
        {
            var mappingSettings = GetPluginOrFail<MappingSettings>();
            var endpointSettings = GetPluginOrFail<EndpointSettings>();
            var webApiSettings = GetPluginOrFail<WebApiSettings>(endpointSettings.EndpointTo);

            service = new BrightcoveService(webApiSettings.AccountId, webApiSettings.ClientId, webApiSettings.ClientSecret);

            Video video = null;

            try
            {
                video = (Video)pipelineContext.GetObjectFromPipelineContext(mappingSettings.TargetObjectLocation);
                ItemModel itemModel = (ItemModel)pipelineContext.GetObjectFromPipelineContext(mappingSettings.SourceObjectLocation);
                Item item = Sitecore.Context.ContentDatabase.GetItem(itemModel.GetItemId().ToString());

                DateTime lastSyncTime = DateTime.UtcNow;
                DateField lastModifiedTime = item.Fields["__Updated"];
                bool isNewVideo = string.IsNullOrWhiteSpace(item["LastSyncTime"]);

                if (!isNewVideo)
                {
                    lastSyncTime = DateTime.Parse(item["LastSyncTime"]);
                }

                if (HandleDelete(itemModel, item, video))
                {
                    return;
                }

                ApplyMappings(mappingSettings.MappingSets, itemModel, video);

                //If the brightcove item has been modified since the last sync (or is new) then send the updates to brightcove
                //Unless the brightcove model has already been modified since the last sync (presumably outside of Sitecore)
                if (isNewVideo || lastModifiedTime.DateTime > lastSyncTime)
                {
                    if (isNewVideo || video.LastModifiedDate < lastSyncTime)
                    {
                        UpdateAsset(video);

                        if (isNewVideo)
                        {
                            item.Editing.BeginEdit();
                            item["LastSyncTime"] = DateTime.UtcNow.ToString();
                            item.Editing.EndEdit();
                        }

                        LogDebug($"Updated the brightcove model '{video.Id}'");
                    }
                    else
                    {
                        LogWarn($"Ignored changes made to brightcove item '{item.ID}' because the brightcove model '{video.Id}' has been modified since last sync. Please run the pull pipeline to get the latest changes");
                    }
                }
                else
                {
                    LogDebug($"Ignored the brightcove item '{item.ID}' because it has not been updated since last sync");
                }
            }
            catch(Exception ex)
            {
                LogError($"An unexpected error occured updating the model '{video?.Id}'", ex);
            }
        }

        private void ApplyMappings(IEnumerable<IMappingSet> mappingSets, ItemModel itemModel, Video video)
        {
            foreach (IMappingSet mappingSet in mappingSets)
            {
                var mappingContext = Mapper.ApplyMapping(mappingSet, itemModel, video);

                if (Mapper.HasErrors(mappingContext))
                {
                    LogError($"Failed to apply mapping to the model '{itemModel.GetItemId()}': {Mapper.GetFailedMappings(mappingContext)}");
                }
                else
                {
                    LogDebug($"Applied mapping to the model '{itemModel.GetItemId()}'");
                }
            }
        }

        public bool HandleDelete(ItemModel itemModel, Item item, Video video)
        {
            //The item has been marked for deletion in Sitecore
            if ((string)itemModel["Delete"] == "1")
            {
                LogInfo($"Deleting the brightcove model '{video.Id}' because it has been marked for deletion in Sitecore");
                service.DeleteVideo(video.Id);

                LogInfo($"Deleting the brightcove item '{item.ID}' because it has been marked for deleteion in Sitecore");
                item.Delete();

                return true;
            }

            return false;
        }

        public void UpdateAsset(Video video)
        {
            try
            {
                service.UpdateVideo(video);
            }
            //This is hacky fix to silent ignore invalid custom fields
            //This should be removed when a more permant solution is found
            catch (HttpStatusException ex)
            {
                if ((int)ex.Response.StatusCode != 422)
                    throw ex;

                string message = ex.Response.Content.ReadAsString();

                if (!message.Contains("custom_fields"))
                    throw ex;

                //Rerun with the invalid custom fields removed so the rest of the updates are made
                video.CustomFields = null;
                UpdateAsset(video);
                return;
            }
        }
    }
}
