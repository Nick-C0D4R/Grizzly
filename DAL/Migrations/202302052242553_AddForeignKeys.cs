namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "DrugId", "dbo.Drugs");
            DropForeignKey("dbo.Carts", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Carts", new[] { "DrugId" });
            DropIndex("dbo.Carts", new[] { "Order_Id" });
            AddColumn("dbo.Drugs", "Cart_Id", c => c.Int());
            AddColumn("dbo.Orders", "CartId", c => c.Int(nullable: false));
            CreateIndex("dbo.Drugs", "Cart_Id");
            CreateIndex("dbo.Orders", "OfficeId");
            CreateIndex("dbo.Orders", "CartId");
            AddForeignKey("dbo.Drugs", "Cart_Id", "dbo.Carts", "Id");
            AddForeignKey("dbo.Orders", "CartId", "dbo.Carts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "OfficeId", "dbo.FarmacyOffices", "Id", cascadeDelete: true);
            DropColumn("dbo.Carts", "DrugId");
            DropColumn("dbo.Carts", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Order_Id", c => c.Int());
            AddColumn("dbo.Carts", "DrugId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "OfficeId", "dbo.FarmacyOffices");
            DropForeignKey("dbo.Orders", "CartId", "dbo.Carts");
            DropForeignKey("dbo.Drugs", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.Orders", new[] { "CartId" });
            DropIndex("dbo.Orders", new[] { "OfficeId" });
            DropIndex("dbo.Drugs", new[] { "Cart_Id" });
            DropColumn("dbo.Orders", "CartId");
            DropColumn("dbo.Drugs", "Cart_Id");
            CreateIndex("dbo.Carts", "Order_Id");
            CreateIndex("dbo.Carts", "DrugId");
            AddForeignKey("dbo.Carts", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.Carts", "DrugId", "dbo.Drugs", "Id", cascadeDelete: true);
        }
    }
}
