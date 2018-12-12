using CrawlingUtilities;
using Logger;
using MiscUtilities;
using SearchUtilities;
using System.Threading;
using System.Threading.Tasks;

namespace ProductBackgroundEngine
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Parallel.Invoke(
                 () => StartSearchUtilities(),
                 () => StartMainCrawl()
               );

            

        }

        private static void StartSearchUtilities()
        {
            while (true)
            {
                GenericLogger.Info("Starting Search Utilities...");
                var model = CrawlSettingsHelper.LoadCrawlSettings("Search");

                SearchUtilitiesStarter.StartSearching(model);

                Thread.Sleep(3000);

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
