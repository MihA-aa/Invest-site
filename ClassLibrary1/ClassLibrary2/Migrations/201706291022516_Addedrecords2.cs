namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedrecords2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Records", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Records", "DateTime");
        }
    }
}
