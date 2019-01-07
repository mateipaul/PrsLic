namespace DatabaseModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testmigrationerrorfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produs", "Sters", c => c.Boolean());
            DropColumn("dbo.Produs", "Stert");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produs", "Stert", c => c.Boolean());
            DropColumn("dbo.Produs", "Sters");
        }
    }
}
