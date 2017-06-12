namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIdInCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Portfolios", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Profiles", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Portfolios", new[] { "Customer_Id" });
            DropIndex("dbo.Profiles", new[] { "Customer_Id" });
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Portfolios", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Profiles", "Customer_Id", c => c.Int());
            AddPrimaryKey("dbo.Customers", "Id");
            CreateIndex("dbo.Portfolios", "Customer_Id");
            CreateIndex("dbo.Profiles", "Customer_Id");
            AddForeignKey("dbo.Portfolios", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Profiles", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Portfolios", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Profiles", new[] { "Customer_Id" });
            DropIndex("dbo.Portfolios", new[] { "Customer_Id" });
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Profiles", "Customer_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Portfolios", "Customer_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Customers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Customers", "Id");
            CreateIndex("dbo.Profiles", "Customer_Id");
            CreateIndex("dbo.Portfolios", "Customer_Id");
            AddForeignKey("dbo.Profiles", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Portfolios", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
