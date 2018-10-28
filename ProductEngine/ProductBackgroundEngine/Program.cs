using Logger;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace ProductBackgroundEngine
{
    class Program
    {
        
        static void Main(string[] args)
        {

            GenericLogger.Info("Application Started");

            CrawlSettingsHelper.LoadCrawlSettings();


           
        }

        
    }
}
