using AllertCheckUtilities;
using CrawlingUtilities;
using Logger;
using MiscUtilities;
using SearchUtilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductBackgroundEngine
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Parallel.Invoke(
                 //() => StartSearchUtilities(),
                 () => StartMainCrawl()//,
                 //() => StartAllertCheck()
               );

            

        }

        private static void StartAllertCheck()
        {
            while (true)
            {
                Thread.Sleep(2 * 1000);
                AllertCheckUtils utils = new AllertCheckUtils();
                utils.StartAllertCheck();

                Thread.Sleep(4 * 3600 * 1000);
            }
        }

        private static void StartSearchUtilities()
        {
            while (true)
            {
                var model = CrawlSettingsHelper.LoadCrawlSettings("Search");

                SearchUtilitiesStarter.StartSearching(model);

                

            }
        }

        private static void StartMainCrawl()
        {
            while (true)
            {


                GenericLogger.Info("Starting CrawlUtilities");

                var model = CrawlSettingsHelper.LoadCrawlSettings("Update");

                MainCrawlStarter.CrawlStart(model);

                GenericLogger.Info($"Update process finished starting again in 12 hours");

                Thread.Sleep(1000 * 60 * 60 * 12);
            }
        }
    }
}
