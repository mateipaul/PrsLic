using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesModels
{


    public class CrawlingTag
    {
        public string UrlTag { get; set; }
        public string ProductTag { get; set; }
        public string ProductNameTag { get; set; }
    }

    public class Retailer
    {
        public string RetailerName { get; set; }
        public List<CrawlingTag> CrawlingTags { get; set; }
    }

    public class CrawlSettingsModel
    {
        public List<Retailer> Retailers { get; set; }
    }
}
