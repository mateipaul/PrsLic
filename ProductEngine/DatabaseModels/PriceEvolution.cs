using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class PriceEvolution
    {
        public Guid Id { get; set; }
        public string Price { get; set; }
        public Guid ProductId { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
