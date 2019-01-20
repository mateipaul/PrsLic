using DatabaseModels;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllertCheckUtilities
{
    public class AllertCheckUtils
    {
        public void StartAllertCheck()
        {
            try
            {

                using (var dbModel = new DbModelContext())
                {
                    var followInformation = GetFollowedInformation(dbModel);

                    CheckFollowedInformation(ref followInformation);

                    dbModel.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void CheckFollowedInformation( ref List<UrmarireProdus> followInformation)
        {
            foreach (var information in followInformation)
            {
                try
                {
                    decimal followPrice = information.Limita_pret;
                    decimal currentPrice = information.Produs.Pret;

                    if (followPrice <= currentPrice)
                    {
                        if ((!information.UtilizatorNotificat) || (information.UtilizatorNotificat && information.DataNotificarii.Value.ToShortDateString().Equals(DateTime.UtcNow.AddDays(-2).ToShortDateString())))
                        {

                            EmailHelper.SendEmail(destination: information.Utilizator.Email,
                                subject: $"Alerta Pret pentru produs {information.Produs.Denumire.Substring(0, 50)}...",
                                body: GenerateBodyFromInfo(information));
                            
                            information.UtilizatorNotificat = true;
                            information.DataNotificarii = DateTime.UtcNow;
                        }

                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void CheckForEmail(ref UrmarireProdus information)
        {
            decimal followPrice = information.Limita_pret;
            decimal currentPrice = information.Produs.Pret;

            if (followPrice <= currentPrice)
            {
                if ((!information.UtilizatorNotificat) ||(information.UtilizatorNotificat && information.DataNotificarii.Value.Equals(DateTime.UtcNow.AddDays(-1))))
                {

                    EmailHelper.SendEmail(destination: information.Utilizator.Email,
                        subject: $"Alerta Pret pentru produs {information.Produs.Denumire.Substring(0, 50)}...",
                        body: GenerateBodyFromInfo(information));

                    information.UtilizatorNotificat = true;
                    information.DataNotificarii = DateTime.UtcNow;
                }
                
            }
        }

        private string GenerateBodyFromInfo(UrmarireProdus information)
        {
            var body = new StringBuilder($"<h2>Buna Ziua <strong>{information.Utilizator.Nume_Utilizator}</strong></h2>");

            try
            {
                body.Append("\n");
                body.Append("<h2>Te anuntam ca produsul pe care tu il urmaresti a ajuns la un pret foarte bun</h2>");
                body.Append("\n");
                body.Append($"<a href =\"{ information.Produs.Url}\"><h2>{ information.Produs.Denumire}</h2></a>");
                body.Append("\n");
                body.Append($"<img src=\"{information.Produs.Url_Imagine}\"></img>");
                body.Append("\n");
                body.Append($"<h1>{information.Produs.Pret} lei</h1>");
                body.Append("\n");
            }
            catch (Exception ex)
            {
                
            }

            return body.ToString();

        }

        private List<UrmarireProdus> GetFollowedInformation(DbModelContext dbContext)
        {
            var information = new List<UrmarireProdus>();
            
                 information = (from info in dbContext.UrmarireProdus
                                  join user in dbContext.Utilizator on info.Id_Utilizator equals user.Id
                                  join prodct in dbContext.Produs on info.Id_Produs equals prodct.Id
                                  where info.Invalid == false
                                 select info ).Include(p => p.Utilizator).Include(p => p.Produs).ToList();


            return information;
        }
    }
}
