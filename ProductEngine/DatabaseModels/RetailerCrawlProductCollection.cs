using Logger;
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
        private Vanzator retailer;

        public RetailerCrawlProductCollection(string retailerName)
        {
            GetRetailerID(retailerName);
            GetRetailer(RetailerID);
        }

        private void GetRetailer(Guid retailerID)
        {
            using (DbModelContext dbContext = new DbModelContext())
            {
                if (retailerID != new Guid())
                    retailer = dbContext.Vanzator.Where(ret => ret.Id.Equals(RetailerID)).FirstOrDefault();
            }
        }

        private void GetRetailerID(string retailerName)
        {
            using (DbModelContext dbContext = new DbModelContext())
            {
                RetailerID = dbContext.Vanzator.Where(m => m.Nume.Equals(retailerName, StringComparison.InvariantCultureIgnoreCase)).Select(m => m.Id).FirstOrDefault();
            }
        }

        public void AddProduct(Produs prd)
        {
            prd.Id_Vanzator = RetailerID;
            prd.Cod_Denumire_Produs = GetDescriptionCode(prd.Denumire);
            prd.Id = StringToGuid(prd.Url);
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

        private Guid StringToGuid(string url)
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

        public void SaveProducts(string givenMessage)
        {
            using (DbModelContext dbContext = new DbModelContext())
            {
                string searchCode = GetSearchCode(givenMessage);
                foreach (var item in this)
                {
                    try
                    {
                        if (dbContext.Produs.Any(prd => prd.Id.Equals(item.Id)))
                            dbContext.EvolutiaPretului.Add(item.EvolutiaPretului.OrderByDescending(m=>m.Data_Actualizare).First());
                        else
                        {
                            dbContext.Produs.Add(item);
                        }



                        dbContext.AparitieProdus.Add(new AparitieProdus
                        {
                            Id_Cautare = StringToGuid(givenMessage)
                                                                         ,
                            Id_Produs = item.Id,
                            Id = StringToGuid($"{StringToGuid(givenMessage).ToString()}{item.Id.ToString()})")
                        });


                        dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        GenericLogger.Error($"Batch failed to insert {ex.Message} \n {ex.InnerException.InnerException.Message}");
                    }
                }
            }
        }
        private static string GetSearchCode(string stringToSearch)
        {
            var pieces = stringToSearch.Trim().ToUpper().Split(' ');
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

    }
}

