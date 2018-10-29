using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace CrawlingUtilities
{
    public class MainCrawlStarter
    {

        public MainCrawlStarter (CrawlSettingsModel model)
        {
            try
            {
                Parallel.ForEach
                (model.Retailers,
                new ParallelOptions() { MaxDegreeOfParallelism = 4 },
                m => StartCrawling(m)
                );
            }
            catch (Exception ex)
            {
                GenericLogger.Error("Exception thrown at MainCrawlStarter", ex);
            }
            
        }

        private void StartCrawling(RetailerConfiguration retailer)
        {
            try
            {
                CrawlDownloader downloader = new CrawlDownloader(retailer);

                downloader.StartCrawling();
            }
            catch (Exception ex)
            {
                GenericLogger.Error($"Exception popped at StartCrawling for {retailer.RetailerName}", ex);
            }
            
        }
    }
}
