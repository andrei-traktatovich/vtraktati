namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobPartAddWorkInHouse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobParts", "WorkInHouse", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobParts", "WorkInHouse");
        }
    }
}
