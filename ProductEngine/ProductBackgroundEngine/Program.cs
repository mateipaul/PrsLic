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
                () => StartSearchUtilities()
                //, () => StartMainCrawl()
               );

            

        }

        private static void StartSearchUtilities()
        {
            while (true)
            {
                GenericLogger.Info("Starting Search Utilities...");
                var model = CrawlSettingsHelper.LoadCrawlSettings();

                SearchUtilitiesStarter.StartSearching(model);



            }
        }

        private static void StartMainCrawl()
        {
            while (true)
            {


                GenericLogger.Info("Starting CrawlUtilities");

                var model = CrawlSettingsHelper.LoadCrawlSettings();

                MainCrawlStarter.CrawlStart(model);

                Thread.Sleep(1000 * 60 * 60 * 12);
            }
        }
    }
}
