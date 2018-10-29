using DatabaseModels;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace CrawlingUtilities
{
    public class CrawlDownloader
    {
        private RetailerConfiguration Retailer;
        private List<Manufacturer> manufacturers;
        private HtmlDocumentUtilities DocumentUtilities;

        public CrawlDownloader(RetailerConfiguration retailerConfiguration)
        {
            Retailer = retailerConfiguration;
            DocumentUtilities = new HtmlDocumentUtilities();
        }


        public void StartCrawling()
        {
            NavigateToSearchPage();

        }

        private void NavigateToSearchPage()
        {
           
        }
    }
}
