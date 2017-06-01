namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewChangea1qswasdss : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "ViewTemplate_Id1", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "ViewTemplate_Id", newName: "ViewTemplate_Id1");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "__mig_tmp__0", newName: "ViewTemplate_Id");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_ViewTemplate_Id1", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_ViewTemplate_Id", newName: "IX_ViewTemplate_Id1");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "__mig_tmp__0", newName: "IX_ViewTemplate_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_ViewTemplate_Id", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_ViewTemplate_Id1", newName: "IX_ViewTemplate_Id");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "__mig_tmp__0", newName: "IX_ViewTemplate_Id1");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "ViewTemplate_Id", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "ViewTemplate_Id1", newName: "ViewTemplate_Id");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "__mig_tmp__0", newName: "ViewTemplate_Id1");
        }
    }
}
