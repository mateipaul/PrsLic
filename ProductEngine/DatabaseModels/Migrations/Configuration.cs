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
            context.Utilizator.Add(new Utilizator { Id = Guid.NewGuid(), Email = "admin@admin.com", Numar_Telefon = "074962682", Nume_Utilizator = "admin", Parola = "admin", Porecla = "admin", Rol = "1" });
            context.Vanzator.Add(new Vanzator { Cod_Tara = "RO", Delimitator_Zecimala = ",", Id = Guid.NewGuid(), Nume = "Cel", Site = "https://www.cel.ro/", Sters = false });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
