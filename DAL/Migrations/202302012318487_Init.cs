namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserLogin = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        DrugId = c.Int(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drugs", t => t.DrugId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.DrugId)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Drugs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrugName = c.String(),
                        Description = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProducerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .Index(t => t.ProducerId);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProducerName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfficeId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FarmacyOffices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FarmacyTitle = c.String(),
                        FarmacyAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserSurname = c.String(),
                        RoleId = c.Int(nullable: false),
                        Login = c.String(),
                        PhoneNumber = c.String(),
                        Password = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Carts", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Carts", "DrugId", "dbo.Drugs");
            DropForeignKey("dbo.Drugs", "ProducerId", "dbo.Producers");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Drugs", new[] { "ProducerId" });
            DropIndex("dbo.Carts", new[] { "Order_Id" });
            DropIndex("dbo.Carts", new[] { "DrugId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.FarmacyOffices");
            DropTable("dbo.Orders");
            DropTable("dbo.Producers");
            DropTable("dbo.Drugs");
            DropTable("dbo.Carts");
        }
    }
}
