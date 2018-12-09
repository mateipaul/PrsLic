using Logger;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace SearchUtilities
{
    public class SerachUtilitiesStarter
    {
        
        public static void StartSearching(CrawlSettingsModel model)
        {
            if (AzureQueueUtils.Instance.QueueHasItems())
            {

            }
            else
            {
                GenericLogger.Info("Queue Has NO items to be searched");
            }
        }

        
    }
}
