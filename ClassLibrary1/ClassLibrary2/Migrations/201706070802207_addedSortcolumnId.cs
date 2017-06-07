namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSortcolumnId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ViewTemplates", name: "SortColumn_Id", newName: "SortColumnId");
            RenameIndex(table: "dbo.ViewTemplates", name: "IX_SortColumn_Id", newName: "IX_SortColumnId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ViewTemplates", name: "IX_SortColumnId", newName: "IX_SortColumn_Id");
            RenameColumn(table: "dbo.ViewTemplates", name: "SortColumnId", newName: "SortColumn_Id");
        }
    }
}
