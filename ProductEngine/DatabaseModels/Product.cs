using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Descriprion { get; set; }
        public string Price { get; set; }
        public string Stock { get; set; }
        public string ImageUrl { get; set; }
        public bool Deleted { get; set; }

    }
}
