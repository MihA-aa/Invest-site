namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnViewTemplate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ViewTemplateColumns", "FormatId", "dbo.Formats");
            DropIndex("dbo.ViewTemplateColumns", new[] { "FormatId" });
            AddColumn("dbo.ViewTemplateColumns", "ColumnFormat_Id", c => c.Int());
            CreateIndex("dbo.ViewTemplateColumns", "ColumnFormat_Id");
            AddForeignKey("dbo.ViewTemplateColumns", "ColumnFormat_Id", "dbo.ColumnFormats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewTemplateColumns", "ColumnFormat_Id", "dbo.ColumnFormats");
            DropIndex("dbo.ViewTemplateColumns", new[] { "ColumnFormat_Id" });
            DropColumn("dbo.ViewTemplateColumns", "ColumnFormat_Id");
            CreateIndex("dbo.ViewTemplateColumns", "FormatId");
            AddForeignKey("dbo.ViewTemplateColumns", "FormatId", "dbo.Formats", "Id");
        }
    }
}
