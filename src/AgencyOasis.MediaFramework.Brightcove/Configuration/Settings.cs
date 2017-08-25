﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.SecurityModel.License;

namespace AgencyOasis.MediaFramework.Brightcove.Configuration
{
    public static class Settings
    {
        public static string CommandRoutePrefix
        {
            get
            {
                return Sitecore.Configuration.Settings.GetSetting("Brightcove.CommandRoutePrefix", string.Empty).TrimStart('/').TrimEnd('/');
            }
        }

        public static int ImportLimit
        {
            get
            {
                return Sitecore.Configuration.Settings.GetIntSetting("Brightcove.ImportLimit", 20);
            }
        }

        public static string BrightcoveTextTracksMimeTypes
        {
            get
            {
                return Sitecore.Configuration.Settings.GetSetting("Brightcove.TextTracksMimeTypes", "{application/octet-stream}");
            }
        }

        public static string FileDownloadRouteTemplate
        {
            get { return CommandRoutePrefix + Constants.BrightcoveFileDownloadRouteTemplate; }
        }

        public static string CustomFieldsRouteTemplate
        {
            get { return CommandRoutePrefix + Constants.BrightcoveVideoCustomFieldsRouteTemplate; }
        }

        public static string TextTracksRouteTemplate
        {
            get { return CommandRoutePrefix + Constants.BrightcoveVideoTextTracksRouteTemplate; }
        }

        public static string IngestionCallbackRouteTemplate
        {
            get { return CommandRoutePrefix + Constants.BrightcoveIngestionCallbackRouteTemplate; }
        }

        public static string DefaultRouteTemplate
        {
            get { return CommandRoutePrefix + Constants.DefaultRouteTemplate; }
        }

        public static string FileDownloadUrl(string baseUrl, string fileId)
        {
            return string.Format("{0}/{1}", baseUrl.TrimEnd('/'), FileDownloadRouteTemplate.Replace("{fileId}", fileId));
        }

        public static string IngestionCallbackUrl(string baseUrl, string requestId)
        {
            return string.Format("{0}/{1}", baseUrl.TrimEnd('/'), IngestionCallbackRouteTemplate.Replace("{operationId}", requestId));
        }

        public static string CustomFieldsApplicationUrl(string accountItemId, string videoId)
        {
            return CustomFieldsRouteTemplate
                .Replace("{accountItemId}", accountItemId)
                .Replace("{videoId}", videoId);
        }

        public static bool AnalyticsEnabled
        {
            get
            {
                var analyticsEnabled = Sitecore.Configuration.Settings.Analytics.Enabled;
                if (!analyticsEnabled)
                {
                    var xdbEnabled = Sitecore.Configuration.Settings.GetBoolSetting("Xdb.Enabled", false);
                    var hasLicense = License.HasModule("Sitecore.xDB.Base") || License.HasModule("Sitecore.xDB.Plus") || (License.HasModule("Sitecore.xDB.Premium") || License.HasModule("Sitecore.xDB.Base.Cloud")) || (License.HasModule("Sitecore.xDB.Plus.Cloud") || License.HasModule("Sitecore.xDB.Premium.Cloud")) || License.HasModule("Sitecore.xDB.Enterprise.Cloud") || License.HasModule("Sitecore.OMS");

                    analyticsEnabled = xdbEnabled && hasLicense;
                }
                return analyticsEnabled;
            }
        }
    }
}