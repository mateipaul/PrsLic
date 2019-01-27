namespace DatabaseModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removekeysfromproductfollow : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UrmarireProdus");
            AddPrimaryKey("dbo.UrmarireProdus", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UrmarireProdus");
            AddPrimaryKey("dbo.UrmarireProdus", new[] { "Id", "Id_Produs", "Id_Utilizator", "Limita_pret" });
        }
    }
}
