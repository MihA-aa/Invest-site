namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12312wqhжюwer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FormatColumnFormats", "Format_Id", "dbo.Formats");
            DropForeignKey("dbo.FormatColumnFormats", "ColumnFormat_Id", "dbo.ColumnFormats");
            DropIndex("dbo.FormatColumnFormats", new[] { "Format_Id" });
            DropIndex("dbo.FormatColumnFormats", new[] { "ColumnFormat_Id" });
            AddColumn("dbo.ColumnFormats", "Format_Id", c => c.Int());
            CreateIndex("dbo.ColumnFormats", "Format_Id");
            AddForeignKey("dbo.ColumnFormats", "Format_Id", "dbo.Formats", "Id");
            DropTable("dbo.FormatColumnFormats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FormatColumnFormats",
                c => new
                    {
                        Format_Id = c.Int(nullable: false),
                        ColumnFormat_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Format_Id, t.ColumnFormat_Id });
            
            DropForeignKey("dbo.ColumnFormats", "Format_Id", "dbo.Formats");
            DropIndex("dbo.ColumnFormats", new[] { "Format_Id" });
            DropColumn("dbo.ColumnFormats", "Format_Id");
            CreateIndex("dbo.FormatColumnFormats", "ColumnFormat_Id");
            CreateIndex("dbo.FormatColumnFormats", "Format_Id");
            AddForeignKey("dbo.FormatColumnFormats", "ColumnFormat_Id", "dbo.ColumnFormats", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FormatColumnFormats", "Format_Id", "dbo.Formats", "Id", cascadeDelete: true);
        }
    }
}
