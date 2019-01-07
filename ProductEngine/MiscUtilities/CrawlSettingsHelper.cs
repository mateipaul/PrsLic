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

        public static CrawlSettingsModel LoadCrawlSettings(string usage)
        {
           

            AzureBlobUtils.Instance.DownloadBlob("CrawlingTags.json", "crawlsettings",usage);

            try
            {
                string text = string.Empty;
                using (StreamReader reader =  new StreamReader(Path.Combine(Environment.CurrentDirectory,"BlobFiles",usage,"CrawlingTags.json")))
                {
                    text = reader.ReadToEnd();
                }

                model = JsonUtilities.Instance.DeserializeJson<CrawlSettingsModel>(text);
                
            }
            catch (Exception ex)
            {
                GenericLogger.Error($"Exception {ex.Message} popped when trying to parse crawling Settings",ex);
            }
            return model;
        }

    }
}
