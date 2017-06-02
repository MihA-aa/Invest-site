namespace DALEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Positions", "LastUpdateDate", c => c.DateTime());
            AddColumn("dbo.Positions", "LastUpdatePrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Positions", "LastUpdatePrice");
            DropColumn("dbo.Positions", "LastUpdateDate");
        }
    }
}
