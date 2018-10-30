using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesModels
{


    public class CrawlingTags
    {
        public string SearchUrlFormat { get; set; }
        public string ProductCellTag { get; set; }
        public string UrlTag { get; set; }
        public string NextProductPageTag { get; set; }

        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductStock { get; set; }
        public string ProductImage { get; set; }

    }

    public class RetailerConfiguration
    {
        public string RetailerName { get; set; }
        public CrawlingTags CrawlingTags { get; set; }
    }

    public class CrawlSettingsModel
    {
        public List<RetailerConfiguration> Retailers { get; set; }
    }
}
