namespace DatabaseModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedpricefromstringtodecima : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UrmarireProdus");
            AlterColumn("dbo.Produs", "Pret", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.EvolutiaPretului", "Pret", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.UrmarireProdus", "Limita_pret", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddPrimaryKey("dbo.UrmarireProdus", new[] { "Id", "Id_Produs", "Id_Utilizator", "Limita_pret" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UrmarireProdus");
            AlterColumn("dbo.UrmarireProdus", "Limita_pret", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.EvolutiaPretului", "Pret", c => c.String(maxLength: 50));
            AlterColumn("dbo.Produs", "Pret", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.UrmarireProdus", new[] { "Id", "Id_Produs", "Id_Utilizator", "Limita_pret" });
        }
    }
}
