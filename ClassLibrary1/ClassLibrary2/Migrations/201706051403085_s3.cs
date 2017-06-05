namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "FormatId", newName: "Format_Id");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_FormatId", newName: "IX_Format_Id");
            CreateTable(
                "dbo.Columns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Format_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Formats", t => t.Format_Id)
                .Index(t => t.Format_Id);
            
            AddColumn("dbo.ViewTemplateColumns", "Column_Id", c => c.Int());
            AddColumn("dbo.ViewTemplateColumns", "ColumnFormat_Id", c => c.Int());
            CreateIndex("dbo.ViewTemplateColumns", "Column_Id");
            CreateIndex("dbo.ViewTemplateColumns", "ColumnFormat_Id");
            AddForeignKey("dbo.ViewTemplateColumns", "Column_Id", "dbo.Columns", "Id");
            AddForeignKey("dbo.ViewTemplateColumns", "ColumnFormat_Id", "dbo.ColumnFormats", "Id");
            DropColumn("dbo.ViewTemplateColumns", "Column");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ViewTemplateColumns", "Column", c => c.Int(nullable: false));
            DropForeignKey("dbo.ViewTemplateColumns", "ColumnFormat_Id", "dbo.ColumnFormats");
            DropForeignKey("dbo.ViewTemplateColumns", "Column_Id", "dbo.Columns");
            DropForeignKey("dbo.Columns", "Format_Id", "dbo.Formats");
            DropIndex("dbo.Columns", new[] { "Format_Id" });
            DropIndex("dbo.ViewTemplateColumns", new[] { "ColumnFormat_Id" });
            DropIndex("dbo.ViewTemplateColumns", new[] { "Column_Id" });
            DropColumn("dbo.ViewTemplateColumns", "ColumnFormat_Id");
            DropColumn("dbo.ViewTemplateColumns", "Column_Id");
            DropTable("dbo.Columns");
            RenameIndex(table: "dbo.ViewTemplateColumns", name: "IX_Format_Id", newName: "IX_FormatId");
            RenameColumn(table: "dbo.ViewTemplateColumns", name: "Format_Id", newName: "FormatId");
        }
    }
}
