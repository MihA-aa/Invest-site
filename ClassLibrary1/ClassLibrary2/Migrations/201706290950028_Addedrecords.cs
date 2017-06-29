namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedrecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Successfully = c.Boolean(nullable: false),
                        EntitieId = c.Int(nullable: false),
                        UserId = c.String(),
                        Entitie = c.Int(nullable: false),
                        Operation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Records");
        }
    }
}
