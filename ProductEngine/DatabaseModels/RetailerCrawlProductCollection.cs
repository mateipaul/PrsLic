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
    public class RetailerCrawlProductCollection : List<Produs>
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
            RetailerID = dbContext.Vanzator.Where(m => m.Nume.Equals(retailerName, StringComparison.InvariantCultureIgnoreCase)).Select(m => m.Id).FirstOrDefault();
        }

        public void AddProduct(Produs prd)
        {
            prd.Id_Vanzator = RetailerID;
            prd.Cod_Denumire_Produs = GetDescriptionCode(prd.Denumire);
            prd.Id = GetProductID(prd.Url);
            prd.Sters = false;
            prd.EvolutiaPretului.Add(GetNewPriceForEvolution(prd));
            prd.Data_Creat = DateTime.UtcNow;
            this.Add(prd);
        }

        private EvolutiaPretului GetNewPriceForEvolution(Produs product)
        {
            EvolutiaPretului evol = new EvolutiaPretului();
            evol.Id_Produs = product.Id;
            evol.Pret = product.Pret;
            evol.Id = Guid.NewGuid();
            evol.Data_Actualizare = DateTime.UtcNow;
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
                    if (dbContext.Produs.Any(prd => prd.Id.Equals(item.Id)))
                        dbContext.EvolutiaPretului.Add(item.EvolutiaPretului.First());
                    else
                    {
                        dbContext.Produs.Add(item);
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

