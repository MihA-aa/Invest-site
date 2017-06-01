namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewChangea1qswasd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ColumnFormats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Format_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Formats", t => t.Format_Id)
                .Index(t => t.Format_Id);
            
            AddColumn("dbo.Formats", "Name", c => c.String());
            DropColumn("dbo.Formats", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Formats", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.ColumnFormats", "Format_Id", "dbo.Formats");
            DropIndex("dbo.ColumnFormats", new[] { "Format_Id" });
            DropColumn("dbo.Formats", "Name");
            DropTable("dbo.ColumnFormats");
        }
    }
}
