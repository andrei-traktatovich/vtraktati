namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_regions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Regions", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Regions", "Name", c => c.Int(nullable: false));
        }
    }
}
