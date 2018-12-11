using Logger;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class RetailerCrawlProductCollection : List<Product>
    {
        public string RetailerName { get; set; }
        private Guid RetailerID;
        private DbModelContext dbContext;

        public RetailerCrawlProductCollection(string retailerName)
        {
            dbContext = new DbModelContext();
            GetRetailerID(retailerName);

        }

        private void GetRetailerID(string retailerName)
        {
            RetailerID = dbContext.Retailers.Where(m => m.Name.Equals(retailerName, StringComparison.InvariantCultureIgnoreCase)).Select(m => m.Id).FirstOrDefault();
        }

        public void AddProduct(Product prd)
        {
            prd.RetailerID = RetailerID;
            prd.ProductDescriptionCode = GetDescriptionCode(prd.Description);
            prd.Id = GetProductID(prd.Url);
            prd.Deleted = false;
            prd.PriceEvolutions.Add(GetNewPriceForEvolution(prd));
            prd.Created = DateTime.UtcNow;
            this.Add(prd);
        }

        private PriceEvolution GetNewPriceForEvolution(Product product)
        {
            PriceEvolution evol = new PriceEvolution();
            evol.ProductID = product.Id;
            evol.Price = product.Price;
            evol.Id = Guid.NewGuid();
            evol.UpdatedDate = DateTime.UtcNow;
            return evol;
        }

        private Guid GetProductID(string url)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(url));
            return new Guid(data);
        }

        private string GetDescriptionCode(string description)
        {
            var pieces = description.Trim().ToUpper().Split(' ');
            int completeSearchCode = 1;
            foreach (var piece in pieces)
            {
                try
                {
                    completeSearchCode = completeSearchCode ^ piece.GetHashCode();
                }
                catch (Exception ex)
                { }
            }

            return completeSearchCode.ToString();

        }

        public void SaveProducts()
        {

            foreach (var item in this)
            {
                try
                {
                    if (dbContext.Products.Any(prd => prd.Id.Equals(item.Id)))
                        dbContext.PriceEvolutions.Add(item.PriceEvolutions.First());
                    else
                    {
                        dbContext.Products.Add(item);
                    }
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    GenericLogger.Error($"Batch failed to insert {ex.Message} \n {ex.InnerException.InnerException.Message}");
                }
            }
        }
    }
}

