using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Inactive { get; set; }

    }
}
