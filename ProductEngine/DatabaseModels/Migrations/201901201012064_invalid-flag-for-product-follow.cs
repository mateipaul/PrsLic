namespace DatabaseModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invalidflagforproductfollow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrmarireProdus", "Invalid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrmarireProdus", "Invalid");
        }
    }
}
