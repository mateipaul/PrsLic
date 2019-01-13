namespace DatabaseModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpasswords_saltcheie_parolatoutilizator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizator", "CheieParola", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilizator", "CheieParola");
        }
    }
}
