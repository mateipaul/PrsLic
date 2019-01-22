using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace CrawlingUtilities
{
    public static class MainCrawlStarter
    {

        public static void CrawlStart (CrawlSettingsModel model)
        {
            try
            {
                Parallel.ForEach
                (model.Retailers,
                new ParallelOptions() { MaxDegreeOfParallelism = 2 },
                m => StartCrawling(m)
                );
            }
            catch (Exception ex)
            {
                GenericLogger.Error("Exception thrown at MainCrawlStarter", ex);
            }
            
        }

        private static void StartCrawling(RetailerConfiguration retailer)
        {
            CrawlManager manager = new CrawlManager(retailer);
            manager.StartCrawling();
        }
    }
}
