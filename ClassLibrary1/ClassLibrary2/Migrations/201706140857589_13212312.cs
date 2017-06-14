namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13212312 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Profiles", name: "Customer_Id", newName: "CustomerId");
            RenameIndex(table: "dbo.Profiles", name: "IX_Customer_Id", newName: "IX_CustomerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Profiles", name: "IX_CustomerId", newName: "IX_Customer_Id");
            RenameColumn(table: "dbo.Profiles", name: "CustomerId", newName: "Customer_Id");
        }
    }
}
