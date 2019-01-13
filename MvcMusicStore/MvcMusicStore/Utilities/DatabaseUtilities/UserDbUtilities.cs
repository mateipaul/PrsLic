using MvcMusicStore.Utilities.ClassModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Utilities.DatabaseUtilities
{
    public class UserDbUtilities
    {
        internal static Utilizator GetUser(string email)
        {

            using (var dbContext  =  new DbModelContext())
            {
                var user = dbContext.Utilizator.FirstOrDefault(m => m.Email.Equals(email));
                return user;
            }
        }
        internal static void AddUserToDb(UserModel user)
        {
            using (var dbContext = new DbModelContext())
            {
                var crypto = new SimpleCrypto.PBKDF2();

                var encPass = crypto.Compute(user.Parola);

                var tempUser = dbContext.Utilizator.Create();

                tempUser.Id = Guid.NewGuid();
                tempUser.Numar_Telefon = user.Numar_Telefon;
                tempUser.Nume_Utilizator = user.Nume_Utilizator;
                tempUser.Parola = encPass;
                tempUser.Porecla = user.Porecla;
                tempUser.Rol = "0";
                tempUser.Email = user.Email;
                tempUser.CheieParola = crypto.Salt;

                dbContext.Utilizator.Add(tempUser);
                dbContext.SaveChanges();

            }

        }
    }
}