namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewChangea1qswasdssw : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "Format_Id", newName: "FormatId");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_Format_Id", newName: "IX_FormatId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_FormatId", newName: "IX_Format_Id");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "FormatId", newName: "Format_Id");
        }
    }
}
