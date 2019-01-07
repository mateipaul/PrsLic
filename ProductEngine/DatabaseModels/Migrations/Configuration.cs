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

            Utilizator util = new Utilizator() { Id = Guid.NewGuid(), Email = "abc@abc.com", Numar_Telefon = "0749626828", Nume_Utilizator = "admin", Parola = "admin", Porecla = "admin", Rol = "1" };
            context.Utilizator.AddOrUpdate(util);
            context.SaveChanges();

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
