namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewChangea1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Formats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ViewTemplateColumns", "Format_Id", c => c.Int());
            CreateIndex("dbo.ViewTemplateColumns", "Format_Id");
            AddForeignKey("dbo.ViewTemplateColumns", "Format_Id", "dbo.Formats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewTemplateColumns", "Format_Id", "dbo.Formats");
            DropIndex("dbo.ViewTemplateColumns", new[] { "Format_Id" });
            DropColumn("dbo.ViewTemplateColumns", "Format_Id");
            DropTable("dbo.Formats");
        }
    }
}
