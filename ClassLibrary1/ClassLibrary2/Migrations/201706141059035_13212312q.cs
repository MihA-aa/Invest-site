namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13212312q : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Views", "Customer_Id", c => c.Int());
            AddColumn("dbo.ViewTemplates", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Views", "Customer_Id");
            CreateIndex("dbo.ViewTemplates", "Customer_Id");
            AddForeignKey("dbo.Views", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.ViewTemplates", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewTemplates", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Views", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.ViewTemplates", new[] { "Customer_Id" });
            DropIndex("dbo.Views", new[] { "Customer_Id" });
            DropColumn("dbo.ViewTemplates", "Customer_Id");
            DropColumn("dbo.Views", "Customer_Id");
        }
    }
}
