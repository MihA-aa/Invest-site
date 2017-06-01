namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewChangea : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Views",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShowName = c.Boolean(nullable: false),
                        DateFormat = c.Int(nullable: false),
                        MoneyPrecision = c.Int(nullable: false),
                        PercentyPrecision = c.Int(nullable: false),
                        Portfolio_Id = c.Int(),
                        ViewTemplate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_Id)
                .ForeignKey("dbo.ViewTemplates", t => t.ViewTemplate_Id)
                .Index(t => t.Portfolio_Id)
                .Index(t => t.ViewTemplate_Id);
            
            CreateTable(
                "dbo.ViewTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Positions = c.Int(nullable: false),
                        ShowPortfolioStats = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        SortColumn_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ViewTemplateColumns", t => t.SortColumn_Id)
                .Index(t => t.SortColumn_Id);
            
            CreateTable(
                "dbo.ViewTemplateColumns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Column = c.Int(nullable: false),
                        ViewTemplate_Id = c.Int(),
                        ViewTemplate_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ViewTemplates", t => t.ViewTemplate_Id)
                .ForeignKey("dbo.ViewTemplates", t => t.ViewTemplate_Id1)
                .Index(t => t.ViewTemplate_Id)
                .Index(t => t.ViewTemplate_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Views", "ViewTemplate_Id", "dbo.ViewTemplates");
            DropForeignKey("dbo.ViewTemplates", "SortColumn_Id", "dbo.ViewTemplateColumns");
            DropForeignKey("dbo.ViewTemplateColumns", "ViewTemplate_Id1", "dbo.ViewTemplates");
            DropForeignKey("dbo.ViewTemplateColumns", "ViewTemplate_Id", "dbo.ViewTemplates");
            DropForeignKey("dbo.Views", "Portfolio_Id", "dbo.Portfolios");
            DropIndex("dbo.ViewTemplateColumns", new[] { "ViewTemplate_Id1" });
            DropIndex("dbo.ViewTemplateColumns", new[] { "ViewTemplate_Id" });
            DropIndex("dbo.ViewTemplates", new[] { "SortColumn_Id" });
            DropIndex("dbo.Views", new[] { "ViewTemplate_Id" });
            DropIndex("dbo.Views", new[] { "Portfolio_Id" });
            DropTable("dbo.ViewTemplateColumns");
            DropTable("dbo.ViewTemplates");
            DropTable("dbo.Views");
        }
    }
}
