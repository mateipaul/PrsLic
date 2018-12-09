using MvcMusicStore.Utilities.AzureUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MvcMusicStore.Utilities.DatabaseUtilities
{
    public static class SearchDBUtilities
    {
        static DbConnectionString dbContext = new DbConnectionString();

        public static IEnumerable<Product> SearchProductsInDatabase(string stringToSearch)
        {
            string searchIdiomCode = GetSearchCode(stringToSearch);

            var products = dbContext.Products.Where(product => product.ProductDescriptionCode.Equals(searchIdiomCode)).ToList();

            if (products.Count < 1)
            {
                InsertIdiomInDatabase(stringToSearch);
                InsertCompleteIdiomInDatabase(stringToSearch, searchIdiomCode);
                QueueUtilities.InsertIdiomInQueue(stringToSearch);

            }
            
            return products;
        }

        private static void InsertCompleteIdiomInDatabase(string stringToSearch, string searchIdiomCode)
        {
            try
            {
                SearchHistory history = new SearchHistory()
                {
                    Code = searchIdiomCode,
                    Value = stringToSearch,
                    SearchID = StringToGUID(stringToSearch)
                };
                dbContext.SearchHistories.Add(history);
                dbContext.SaveChanges();
            }
            catch { }
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
            SearchHistory tempHistory = new SearchHistory
            {
                SearchID = StringToGUID(piece),
                Value = piece.Trim(),
                Code = piece.Trim().GetHashCode().ToString()
            };
            dbContext.SearchHistories.Add(tempHistory);
            dbContext.SaveChanges();
        }

        static Guid StringToGUID(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return new Guid(data);
        }


    }
}