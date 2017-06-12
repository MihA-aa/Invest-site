namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIdInCustomer11123 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Portfolios", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Profiles", "Customer_Id", "dbo.Customers");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Customers", "Id");
            AddForeignKey("dbo.Portfolios", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Profiles", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Portfolios", "Customer_Id", "dbo.Customers");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Customers", "Id");
            AddForeignKey("dbo.Profiles", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Portfolios", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
