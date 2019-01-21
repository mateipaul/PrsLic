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
            context.Vanzator.Add(new Vanzator { Cod_Tara = "RO", Delimitator_Zecimala = ",", Id = Guid.NewGuid(), Nume = "Cel", Site = "https://www.cel.ro/", Sters = false });
            context.Vanzator.Add(new Vanzator { Cod_Tara = "RO", Delimitator_Zecimala = ",", Id = Guid.NewGuid(), Nume = "PcGarage", Site = "https://www.pcgarage.ro/", Sters = false });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

    }
}
