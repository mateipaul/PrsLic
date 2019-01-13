namespace DatabaseModels.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseModels.DbModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseModels.DbModelContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Utilizator.Add(GetNewUserAdmin());
            context.Vanzator.Add(new Vanzator { Cod_Tara = "RO", Delimitator_Zecimala = ",", Id = Guid.NewGuid(), Nume = "Cel", Site = "https://www.cel.ro/", Sters = false });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        private Utilizator GetNewUserAdmin()
        {
            var crypto = new SimpleCrypto.PBKDF2();

            var encPass = crypto.Compute("admin");

            var tempUser = new Utilizator();

            tempUser.Id = Guid.NewGuid();
            tempUser.Numar_Telefon = "0749626828";
            tempUser.Nume_Utilizator = "admin";
            tempUser.Parola = encPass;
            tempUser.Porecla = "admin";
            tempUser.Rol = "1";
            tempUser.Email = "admin@admin.com";
            tempUser.CheieParola = crypto.Salt;

            return tempUser;

        }
    }
}
