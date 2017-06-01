namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewChangea1qswasdsswwq12 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "FormatId", newName: "Format_Id");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "ViewTemplate_Id1", newName: "ViewTemplateId");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_ViewTemplate_Id1", newName: "IX_ViewTemplateId");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_FormatId", newName: "IX_Format_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_Format_Id", newName: "IX_FormatId");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_ViewTemplateId", newName: "IX_ViewTemplate_Id1");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "ViewTemplateId", newName: "ViewTemplate_Id1");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "Format_Id", newName: "FormatId");
        }
    }
}
