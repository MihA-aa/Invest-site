namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Notes = c.String(),
                        DisplayIndex = c.Int(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                        Visibility = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PercentWins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BiggestWinner = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BiggestLoser = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgGain = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MonthAvgGain = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PortfolioValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SymbolId = c.Int(nullable: false),
                        SymbolType = c.Int(nullable: false),
                        SymbolName = c.String(),
                        Name = c.String(),
                        OpenDate = c.DateTime(nullable: false),
                        OpenPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpenWeight = c.Int(nullable: false),
                        TradeType = c.Int(nullable: false),
                        TradeStatus = c.Int(nullable: false),
                        Dividends = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CloseDate = c.DateTime(),
                        ClosePrice = c.Decimal(precision: 18, scale: 2),
                        CurrentPrice = c.Decimal(precision: 18, scale: 2),
                        Gain = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AbsoluteGain = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxGain = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portfolio_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_Id)
                .Index(t => t.Portfolio_Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Login = c.String(),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Profiles", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Profiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Positions", "Portfolio_Id", "dbo.Portfolios");
            DropForeignKey("dbo.Portfolios", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Profiles", new[] { "Customer_Id" });
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropIndex("dbo.Positions", new[] { "Portfolio_Id" });
            DropIndex("dbo.Portfolios", new[] { "Customer_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Profiles");
            DropTable("dbo.Positions");
            DropTable("dbo.Portfolios");
            DropTable("dbo.Customers");
        }
    }
}
