using MvcMusicStore.Utilities.AzureUtilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;

namespace MvcMusicStore.Utilities.DatabaseUtilities
{
    public static class SearchDBUtilities
    {
        

        public static IEnumerable<Produs> NewSearchProductsInDatabase(string stringToSearch, string order)
        {
            var products = new List<Produs>();
            using (DbModelContext dbContext = new DbModelContext())
            {



                string searchIdiomCode = GetSearchCode(stringToSearch);

                products = (from appear in dbContext.AparitieProdus
                                join search in dbContext.IstoricCautari on appear.Id_Cautare equals search.Id_Cautare
                                join product in dbContext.Produs on appear.Id_Produs equals product.Id
                                join retailer in dbContext.Vanzator on product.Id_Vanzator equals retailer.Id
                                where search.Cod == searchIdiomCode
                                select product).Include(p=>p.Vanzator).ToList();


                if (products.Count <= 1)
                {
                    InsertIdiomInDatabase(stringToSearch);
                    InsertCompleteIdiomInDatabase(stringToSearch, searchIdiomCode);
                    QueueUtilities.InsertIdiomInQueue($"{stringToSearch}|{order}");

                }
            }

            switch (order)
            {
                case "price-asc":
                    {
                        products = products.OrderBy(m => m.Pret).ToList();
                        break;

                    }
                case "price-desc":
                    {
                        products = products.OrderByDescending(m => m.Pret).ToList();
                        break;
                    }
                default:
                    break;
            }

            return products;
        }

        internal static Produs GetProductOnId(Guid productId)
        {
            using (var dbContext = new DbModelContext())
            {
                var prod = dbContext.Produs.Where(m => m.Id.Equals(productId)).FirstOrDefault();
                return prod;
            }
        }

        public static IEnumerable<Produs> SearchProductsInDatabase(string stringToSearch,string order)
        {
            var products = new List<Produs>();

            using (DbModelContext dbContext = new DbModelContext())
            {
                string searchIdiomCode = GetSearchCode(stringToSearch);

                int attempts = 0;
                while (true)
                {


                    products = (from appear in dbContext.AparitieProdus
                                join search in dbContext.IstoricCautari on appear.Id_Cautare equals search.Id_Cautare
                                join product in dbContext.Produs on appear.Id_Produs equals product.Id
                                join retailer in dbContext.Vanzator on product.Id_Vanzator equals retailer.Id
                                where search.Cod == searchIdiomCode
                                select product).Include(p=>p.Vanzator).ToList();
                    if (products.Count >= 1 || attempts > 30)
                    {

                        break;
                    }
                    Thread.Sleep(1000);
                    attempts++;

                } 
            }
            switch (order)
            {
                case "price-asc":
                    {
                        products = products.OrderBy(m => m.Pret).ToList();
                        break;

                    }
                case "price-desc":
                    {
                        products = products.OrderByDescending(m => m.Pret).ToList();
                        break;
                    }
                default:
                    break;
            }
            return products;

        }

        private static void InsertCompleteIdiomInDatabase(string stringToSearch, string searchIdiomCode)
        {
            using (DbModelContext dbContext = new DbModelContext())
            {
                try
                {
                    IstoricCautari history = new IstoricCautari()
                    {
                        Cod = searchIdiomCode,
                        Valoare = stringToSearch,
                        Id_Cautare = StringToGUID(stringToSearch)
                    };
                    dbContext.IstoricCautari.Add(history);
                    dbContext.SaveChanges();
                }
                catch { } 
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
                {}
            }

            return completeSearchCode.ToString();

        }

        private static void InsertIdiomInDatabase(string searchIdiomCode)
        {
            var pieces = searchIdiomCode.Trim().ToUpper().Split(' ');

            foreach (var piece in pieces)
            {
                try
                {
                    AddSearchPieceInDB(piece);
                }
                catch (Exception ex)
                { }
            }
        }

        private static void AddSearchPieceInDB(string piece)
        {
            using (DbModelContext dbContext = new DbModelContext())
            {
                IstoricCautari tempHistory = new IstoricCautari
                {
                    Id_Cautare = StringToGUID(piece),
                    Valoare = piece.Trim(),
                    Cod = piece.Trim().GetHashCode().ToString()
                };
                dbContext.IstoricCautari.Add(tempHistory);
                dbContext.SaveChanges(); 
            }
        }

        static Guid StringToGUID(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return new Guid(data);
        }

        


    }
}