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
        static DbModelContext dbContext = new DbModelContext();

        public static IEnumerable<Produs> SearchProductsInDatabase(string stringToSearch)
        {
            string searchIdiomCode = GetSearchCode(stringToSearch);

            var products = dbContext.Produs.Where(product => product.Cod_Denumire_Produs.Equals(searchIdiomCode)).ToList();

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
            IstoricCautari tempHistory = new IstoricCautari
            {
                Id_Cautare = StringToGUID(piece),
                Valoare = piece.Trim(),
                Cod = piece.Trim().GetHashCode().ToString()
            };
            dbContext.IstoricCautari.Add(tempHistory);
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