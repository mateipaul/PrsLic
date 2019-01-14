using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Utilities.DatabaseUtilities
{
    public static class ProductFollowUtilities
    {
        internal static void AddProductAllert(Guid producId,Guid UserId,string price)
        {
            using (var dbContext = new DbModelContext())
            {
                try
                {
                    UrmarireProdus tempAlert = new UrmarireProdus();

                    tempAlert.Id = Guid.NewGuid();
                    tempAlert.Id_Produs = producId;
                    tempAlert.Id_Utilizator = UserId;
                    tempAlert.Limita_pret = price;

                    dbContext.UrmarireProdus.Add(tempAlert);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {

                }               
            }
            
        }

        internal static List<Produs> GetFollowedProducts(Guid userId)
        {
            List<Produs> tempProds = new List<Produs>();

            using (var dbContext = new DbModelContext())
            {
                tempProds = (from products in dbContext.Produs
                             join followedProducts in dbContext.UrmarireProdus on products.Id equals followedProducts.Id_Produs
                             select products).ToList();
            }
            return tempProds;
        }
    }
}