namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIContextTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActiveIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrugApplicationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrugContraIndications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContraIndication = c.String(),
                        Drug_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drugs", t => t.Drug_Id)
                .Index(t => t.Drug_Id);
            
            CreateTable(
                "dbo.DrugTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrugIndications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Indication = c.String(),
                        Drug_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drugs", t => t.Drug_Id)
                .Index(t => t.Drug_Id);
            
            CreateTable(
                "dbo.DrugApplicationTypeDrugs",
                c => new
                    {
                        DrugApplicationType_Id = c.Int(nullable: false),
                        Drug_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DrugApplicationType_Id, t.Drug_Id })
                .ForeignKey("dbo.DrugApplicationTypes", t => t.DrugApplicationType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Drugs", t => t.Drug_Id, cascadeDelete: true)
                .Index(t => t.DrugApplicationType_Id)
                .Index(t => t.Drug_Id);
            
            AddColumn("dbo.Drugs", "DrugTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Drugs", "ActiveIngredientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Drugs", "DrugTypeId");
            CreateIndex("dbo.Drugs", "ActiveIngredientId");
            AddForeignKey("dbo.Drugs", "ActiveIngredientId", "dbo.ActiveIngredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Drugs", "DrugTypeId", "dbo.DrugTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DrugIndications", "Drug_Id", "dbo.Drugs");
            DropForeignKey("dbo.Drugs", "DrugTypeId", "dbo.DrugTypes");
            DropForeignKey("dbo.DrugContraIndications", "Drug_Id", "dbo.Drugs");
            DropForeignKey("dbo.DrugApplicationTypeDrugs", "Drug_Id", "dbo.Drugs");
            DropForeignKey("dbo.DrugApplicationTypeDrugs", "DrugApplicationType_Id", "dbo.DrugApplicationTypes");
            DropForeignKey("dbo.Drugs", "ActiveIngredientId", "dbo.ActiveIngredients");
            DropIndex("dbo.DrugApplicationTypeDrugs", new[] { "Drug_Id" });
            DropIndex("dbo.DrugApplicationTypeDrugs", new[] { "DrugApplicationType_Id" });
            DropIndex("dbo.DrugIndications", new[] { "Drug_Id" });
            DropIndex("dbo.DrugContraIndications", new[] { "Drug_Id" });
            DropIndex("dbo.Drugs", new[] { "ActiveIngredientId" });
            DropIndex("dbo.Drugs", new[] { "DrugTypeId" });
            DropColumn("dbo.Drugs", "ActiveIngredientId");
            DropColumn("dbo.Drugs", "DrugTypeId");
            DropTable("dbo.DrugApplicationTypeDrugs");
            DropTable("dbo.DrugIndications");
            DropTable("dbo.DrugTypes");
            DropTable("dbo.DrugContraIndications");
            DropTable("dbo.DrugApplicationTypes");
            DropTable("dbo.ActiveIngredients");
        }
    }
}
