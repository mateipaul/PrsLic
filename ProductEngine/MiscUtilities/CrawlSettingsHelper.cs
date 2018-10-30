using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace MiscUtilities
{
    public static class CrawlSettingsHelper
    {
        static CrawlSettingsModel model = new CrawlSettingsModel();

        public static CrawlSettingsModel LoadCrawlSettings()
        {
           

            AzureBlobUtils.Instance.DownloadBlob("CrawlingTags.json", "crawlsettings");

            try
            {
                string text = string.Empty;
                using (StreamReader reader =  new StreamReader(Path.Combine(Environment.CurrentDirectory,"BlobFiles","CrawlingTags.json")))
                {
                    text = reader.ReadToEnd();
                }

                model = JsonUtilities.Instance.DeserializeJson<CrawlSettingsModel>(text);

                GenericLogger.Info("Crawl Settings loaded successfully ");
                
            }
            catch (Exception ex)
            {
                GenericLogger.Error($"Exception {ex.Message} popped when trying to parse crawling Settings",ex);
            }
            return model;
        }

    }
}
