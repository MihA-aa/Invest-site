namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewChangea1qswasds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ColumnFormats", "Format_Id", "dbo.Formats");
            DropIndex("dbo.ColumnFormats", new[] { "Format_Id" });
            CreateTable(
                "dbo.FormatColumnFormats",
                c => new
                    {
                        Format_Id = c.Int(nullable: false),
                        ColumnFormat_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Format_Id, t.ColumnFormat_Id })
                .ForeignKey("dbo.Formats", t => t.Format_Id, cascadeDelete: true)
                .ForeignKey("dbo.ColumnFormats", t => t.ColumnFormat_Id, cascadeDelete: true)
                .Index(t => t.Format_Id)
                .Index(t => t.ColumnFormat_Id);
            
            DropColumn("dbo.ColumnFormats", "Format_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ColumnFormats", "Format_Id", c => c.Int());
            DropForeignKey("dbo.FormatColumnFormats", "ColumnFormat_Id", "dbo.ColumnFormats");
            DropForeignKey("dbo.FormatColumnFormats", "Format_Id", "dbo.Formats");
            DropIndex("dbo.FormatColumnFormats", new[] { "ColumnFormat_Id" });
            DropIndex("dbo.FormatColumnFormats", new[] { "Format_Id" });
            DropTable("dbo.FormatColumnFormats");
            CreateIndex("dbo.ColumnFormats", "Format_Id");
            AddForeignKey("dbo.ColumnFormats", "Format_Id", "dbo.Formats", "Id");
        }
    }
}
