namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12312wqhжюwer123 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ViewTemplateColumns", "FormatId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ViewTemplateColumns", "FormatId", c => c.Int());
        }
    }
}
