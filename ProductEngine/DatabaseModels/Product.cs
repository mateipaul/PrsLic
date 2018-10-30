using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class Product
    {
        private Guid Id { get; set; }
        private bool Deleted { get; set; }

        public string Url { get; private set; }
        public string Descriprion { get; private set; }
        public string Price { get; set; }
        public string Stock { get; set; }
        public string ImageUrl { get; set; }



        public Product(string url)
        {
            this.Id = new Guid();
            this.Url = url;
            this.Deleted = false;
        }

        public override bool Equals(object obj)
        {
            var toCompare = obj as Product;
            return this.Url.Equals(toCompare.Url);
        }
        public override int GetHashCode()
        {
            return this.Url.GetHashCode();
        }

        public void SetDescription(string description)
        {
            Descriprion = description;
        }
    }
}
