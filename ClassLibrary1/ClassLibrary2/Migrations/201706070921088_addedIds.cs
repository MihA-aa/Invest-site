namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIds : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Views", name: "ViewTemplate_Id", newName: "ViewTemplateId");
            RenameColumn(table: "dbo.Views", name: "Portfolio_Id", newName: "PortfolioId");
            RenameIndex(table: "dbo.Views", name: "IX_ViewTemplate_Id", newName: "IX_ViewTemplateId");
            RenameIndex(table: "dbo.Views", name: "IX_Portfolio_Id", newName: "IX_PortfolioId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Views", name: "IX_PortfolioId", newName: "IX_Portfolio_Id");
            RenameIndex(table: "dbo.Views", name: "IX_ViewTemplateId", newName: "IX_ViewTemplate_Id");
            RenameColumn(table: "dbo.Views", name: "PortfolioId", newName: "Portfolio_Id");
            RenameColumn(table: "dbo.Views", name: "ViewTemplateId", newName: "ViewTemplate_Id");
        }
    }
}
