using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    class Retailer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Homepage { get; set; }
        public string CountryCode { get; set; }
        public string DecimalSeparator { get; set; }
        public bool Deleted { get; set; }

    }
}
