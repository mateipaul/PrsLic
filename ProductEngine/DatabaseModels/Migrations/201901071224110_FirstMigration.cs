namespace DatabaseModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EvolutiaPretului",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Pret = c.String(maxLength: 50),
                        Id_Produs = c.Guid(),
                        Data_Actualizare = c.DateTime(storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produs", t => t.Id_Produs, cascadeDelete: true)
                .Index(t => t.Id_Produs);
            
            CreateTable(
                "dbo.Produs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Url = c.String(nullable: false, maxLength: 250),
                        Denumire = c.String(maxLength: 250),
                        Pret = c.String(maxLength: 50),
                        Stock = c.String(maxLength: 50),
                        Url_Imagine = c.String(maxLength: 250),
                        Stert = c.Boolean(),
                        Id_Vanzator = c.Guid(),
                        SKU = c.String(maxLength: 50),
                        EAN = c.String(maxLength: 20),
                        Data_Creat = c.DateTime(storeType: "smalldatetime"),
                        Cod_Denumire_Produs = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vanzator", t => t.Id_Vanzator)
                .Index(t => t.Id_Vanzator);
            
            CreateTable(
                "dbo.UrmarireProdus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Id_Produs = c.Guid(nullable: false),
                        Id_Utilizator = c.Guid(nullable: false),
                        Limita_pret = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.Id, t.Id_Produs, t.Id_Utilizator, t.Limita_pret })
                .ForeignKey("dbo.Utilizator", t => t.Id_Utilizator)
                .ForeignKey("dbo.Produs", t => t.Id_Produs)
                .Index(t => t.Id_Produs)
                .Index(t => t.Id_Utilizator);
            
            CreateTable(
                "dbo.Utilizator",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nume_Utilizator = c.String(nullable: false, maxLength: 50),
                        Parola = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        Numar_Telefon = c.String(maxLength: 14),
                        Porecla = c.String(maxLength: 100),
                        Rol = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vanzator",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nume = c.String(maxLength: 150),
                        Site = c.String(maxLength: 250),
                        Cod_Tara = c.String(maxLength: 10),
                        Delimitator_Zecimala = c.String(maxLength: 1),
                        Sters = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IstoricCautari",
                c => new
                    {
                        Id_Cautare = c.Guid(nullable: false),
                        Valoare = c.String(maxLength: 1000),
                        Cod = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id_Cautare);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produs", "Id_Vanzator", "dbo.Vanzator");
            DropForeignKey("dbo.UrmarireProdus", "Id_Produs", "dbo.Produs");
            DropForeignKey("dbo.UrmarireProdus", "Id_Utilizator", "dbo.Utilizator");
            DropForeignKey("dbo.EvolutiaPretului", "Id_Produs", "dbo.Produs");
            DropIndex("dbo.UrmarireProdus", new[] { "Id_Utilizator" });
            DropIndex("dbo.UrmarireProdus", new[] { "Id_Produs" });
            DropIndex("dbo.Produs", new[] { "Id_Vanzator" });
            DropIndex("dbo.EvolutiaPretului", new[] { "Id_Produs" });
            DropTable("dbo.IstoricCautari");
            DropTable("dbo.Vanzator");
            DropTable("dbo.Utilizator");
            DropTable("dbo.UrmarireProdus");
            DropTable("dbo.Produs");
            DropTable("dbo.EvolutiaPretului");
        }
    }
}
