using Logger;
using Microsoft.WindowsAzure.Storage.Queue;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace SearchUtilities
{
    public class SearchUtilitiesStarter
    {
        
        public static void StartSearching(CrawlSettingsModel model)
        {
            if (AzureQueueUtils.Instance.QueueHasItems())
            {
                var message = AzureQueueUtils.Instance.GetQueueItem();

                StartSearch(message, model);
            }
            else
            {
                //GenericLogger.Info("Queue Has NO items to be searched");
            }
        }

        private static void StartSearch(CloudQueueMessage message, CrawlSettingsModel model)
        {
            GenericLogger.Info($"Started searching for message {message.AsString}");

            Parallel.ForEach(model.Retailers
                            , new ParallelOptions() { MaxDegreeOfParallelism = 2 }
                            , retailer => SearchMessageCrawler.StartMessageCrawling(message,retailer)
                            );
        }
    }
}
