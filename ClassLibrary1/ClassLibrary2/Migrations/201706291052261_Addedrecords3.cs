namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedrecords3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Records", "EntityId", c => c.Int(nullable: false));
            AddColumn("dbo.Records", "Entity", c => c.Int(nullable: false));
            DropColumn("dbo.Records", "EntitieId");
            DropColumn("dbo.Records", "Entitie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Records", "Entitie", c => c.Int(nullable: false));
            AddColumn("dbo.Records", "EntitieId", c => c.Int(nullable: false));
            DropColumn("dbo.Records", "Entity");
            DropColumn("dbo.Records", "EntityId");
        }
    }
}
