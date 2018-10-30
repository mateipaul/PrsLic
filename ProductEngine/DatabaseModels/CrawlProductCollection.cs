using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class CrawlProductCollection:HashSet<Product>
    {
        

        public CrawlProductCollection(string retailerName, Manufacturer manufacturer)
        {
            
        }

    }
}
