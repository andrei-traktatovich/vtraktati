namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addToJobTypesIsInternal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobTypes", "IsInternal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobTypes", "IsInternal");
        }
    }
}