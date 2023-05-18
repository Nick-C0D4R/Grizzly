namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FarmacyOffices", newName: "PharmacyOffices");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PharmacyOffices", newName: "FarmacyOffices");
        }
    }
}
