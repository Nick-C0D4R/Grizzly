namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailForUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DrugContraIndications", "Drug_Id", "dbo.Drugs");
            DropForeignKey("dbo.DrugIndications", "Drug_Id", "dbo.Drugs");
            DropIndex("dbo.DrugContraIndications", new[] { "Drug_Id" });
            DropIndex("dbo.DrugIndications", new[] { "Drug_Id" });
            CreateTable(
                "dbo.DrugContraIndicationDrugs",
                c => new
                    {
                        DrugContraIndication_Id = c.Int(nullable: false),
                        Drug_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DrugContraIndication_Id, t.Drug_Id })
                .ForeignKey("dbo.DrugContraIndications", t => t.DrugContraIndication_Id, cascadeDelete: true)
                .ForeignKey("dbo.Drugs", t => t.Drug_Id, cascadeDelete: true)
                .Index(t => t.DrugContraIndication_Id)
                .Index(t => t.Drug_Id);
            
            CreateTable(
                "dbo.DrugIndicationDrugs",
                c => new
                    {
                        DrugIndication_Id = c.Int(nullable: false),
                        Drug_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DrugIndication_Id, t.Drug_Id })
                .ForeignKey("dbo.DrugIndications", t => t.DrugIndication_Id, cascadeDelete: true)
                .ForeignKey("dbo.Drugs", t => t.Drug_Id, cascadeDelete: true)
                .Index(t => t.DrugIndication_Id)
                .Index(t => t.Drug_Id);
            
            AddColumn("dbo.ActiveIngredients", "Milligrams", c => c.Short(nullable: false));
            AddColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.DrugContraIndications", "Drug_Id");
            DropColumn("dbo.DrugIndications", "Drug_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DrugIndications", "Drug_Id", c => c.Int());
            AddColumn("dbo.DrugContraIndications", "Drug_Id", c => c.Int());
            DropForeignKey("dbo.DrugIndicationDrugs", "Drug_Id", "dbo.Drugs");
            DropForeignKey("dbo.DrugIndicationDrugs", "DrugIndication_Id", "dbo.DrugIndications");
            DropForeignKey("dbo.DrugContraIndicationDrugs", "Drug_Id", "dbo.Drugs");
            DropForeignKey("dbo.DrugContraIndicationDrugs", "DrugContraIndication_Id", "dbo.DrugContraIndications");
            DropIndex("dbo.DrugIndicationDrugs", new[] { "Drug_Id" });
            DropIndex("dbo.DrugIndicationDrugs", new[] { "DrugIndication_Id" });
            DropIndex("dbo.DrugContraIndicationDrugs", new[] { "Drug_Id" });
            DropIndex("dbo.DrugContraIndicationDrugs", new[] { "DrugContraIndication_Id" });
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.ActiveIngredients", "Milligrams");
            DropTable("dbo.DrugIndicationDrugs");
            DropTable("dbo.DrugContraIndicationDrugs");
            CreateIndex("dbo.DrugIndications", "Drug_Id");
            CreateIndex("dbo.DrugContraIndications", "Drug_Id");
            AddForeignKey("dbo.DrugIndications", "Drug_Id", "dbo.Drugs", "Id");
            AddForeignKey("dbo.DrugContraIndications", "Drug_Id", "dbo.Drugs", "Id");
        }
    }
}
