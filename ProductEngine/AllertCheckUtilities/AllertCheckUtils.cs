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
            var followInformation = GetFollowedInformation();

            CheckFollowedInformation(followInformation);

        }

        private void CheckFollowedInformation(List<UrmarireProdus> followInformation)
        {
            foreach (var information in followInformation)
            {
                CheckForEmail(information);
            }
        }

        private void CheckForEmail(UrmarireProdus information)
        {
            decimal followPrice = information.Limita_pret;
            decimal currentPrice = information.Produs.Pret;

            if (followPrice <= currentPrice)
            {
                EmailHelper.SendEmail(destination: information.Utilizator.Email,
                    subject: $"Alerta Pret pentru produs {information.Produs.Denumire.Substring(0, 50)}...",
                    body: GenerateBodyFromInfo(information));

                
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

        private List<UrmarireProdus> GetFollowedInformation()
        {
            var information = new List<UrmarireProdus>();
            using (DbModelContext dbContext =  new DbModelContext())
            {
                 information = (from info in dbContext.UrmarireProdus
                                  join user in dbContext.Utilizator on info.Id_Utilizator equals user.Id
                                  join prodct in dbContext.Produs on info.Id_Produs equals prodct.Id
                                 select info).Include(p => p.Utilizator).Include(p => p.Produs).ToList();
                
            }
            return information;
        }
    }
}
