namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ViewTemplateColumns", "ColumnFormat_Id", "dbo.ColumnFormats");
            DropIndex("dbo.ViewTemplateColumns", new[] { "ColumnFormat_Id" });
            DropColumn("dbo.ViewTemplateColumns", "ColumnFormat_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ViewTemplateColumns", "ColumnFormat_Id", c => c.Int());
            CreateIndex("dbo.ViewTemplateColumns", "ColumnFormat_Id");
            AddForeignKey("dbo.ViewTemplateColumns", "ColumnFormat_Id", "dbo.ColumnFormats", "Id");
        }
    }
}
