using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    tempAlert.Limita_pret = decimal.Parse(price);
                    tempAlert.Invalid = false;
                    tempAlert.UtilizatorNotificat = false;
                    tempAlert.DataNotificarii = DateTime.UtcNow;

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
                             where followedProducts.Id_Utilizator == userId
                             where followedProducts.Invalid == false
                             select products).Include(p=>p.Vanzator).ToList();
            }
            return tempProds;
        }

        internal static void RemoveProductAlert(Guid userId, Guid productId)
        {
            try
            {
                using (var dbContext = new DbModelContext())
                {
                    var item = dbContext.UrmarireProdus.Where(m => m.Id_Produs.Equals(productId) && m.Id_Utilizator.Equals(userId)).FirstOrDefault();

                    item.Invalid = true;

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                
            }
        }
    }
}