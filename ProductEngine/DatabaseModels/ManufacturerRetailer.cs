using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    class ManufacturerRetailer
    {
        public Guid ManufacturerId { get; set; }
        public Guid RetailerId { get; set; }
        public bool Deleted { get; set; }
        
    }
}
