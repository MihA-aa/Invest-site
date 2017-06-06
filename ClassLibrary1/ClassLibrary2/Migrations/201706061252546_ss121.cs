namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss121 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "Format_Id", newName: "FormatId");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "Column_Id", newName: "ColumnId");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_Format_Id", newName: "IX_FormatId");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_Column_Id", newName: "IX_ColumnId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_ColumnId", newName: "IX_Column_Id");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_FormatId", newName: "IX_Format_Id");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "ColumnId", newName: "Column_Id");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "FormatId", newName: "Format_Id");
        }
    }
}
