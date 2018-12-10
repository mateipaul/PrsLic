using DatabaseModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UtilitiesModels;

namespace CrawlingUtilities
{
    internal class CrawlManager
    {
        private List<Manufacturer> manufacturers;
        private RetailerConfiguration RetailerConfig;
        

        public CrawlManager(RetailerConfiguration retailer)
        {
            RetailerConfig = retailer;
        }


        public void StartCrawling()
        {
            //manufacturers = DatabaseUtils.Instance.GetManufacturersFor(RetailerConfig.RetailerName);

            //Parallel.ForEach(manufacturers,
            //        new ParallelOptions() { MaxDegreeOfParallelism = 2 },
            //        manuf => GenerateDownloader(manuf));


        }

        private void GenerateDownloader(Manufacturer manuf)
        {
            CrawlDownloader downloader = new CrawlDownloader(RetailerConfig, manuf);

            downloader.StartNavigation();
        }
    }
}