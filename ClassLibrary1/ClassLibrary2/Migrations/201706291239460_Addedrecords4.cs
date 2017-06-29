namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedrecords4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Records", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Records", "Description", c => c.String());
        }
    }
}
