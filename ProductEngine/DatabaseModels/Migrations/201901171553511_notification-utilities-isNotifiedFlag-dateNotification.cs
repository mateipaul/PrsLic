namespace DatabaseModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificationutilitiesisNotifiedFlagdateNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrmarireProdus", "UtilizatorNotificat", c => c.Boolean(nullable: false));
            AddColumn("dbo.UrmarireProdus", "DataNotificarii", c => c.DateTime(storeType: "smalldatetime"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrmarireProdus", "DataNotificarii");
            DropColumn("dbo.UrmarireProdus", "UtilizatorNotificat");
        }
    }
}
