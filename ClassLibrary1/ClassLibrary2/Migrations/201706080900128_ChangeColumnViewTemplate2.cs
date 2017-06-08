namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnViewTemplate2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "ColumnFormat_Id", newName: "ColumnFormatId");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_ColumnFormat_Id", newName: "IX_ColumnFormatId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_ColumnFormatId", newName: "IX_ColumnFormat_Id");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "ColumnFormatId", newName: "ColumnFormat_Id");
        }
    }
}
