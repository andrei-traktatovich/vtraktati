namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_pending_to_jobPartComletionSTatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPartCompletionStatus", "Pending", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobPartCompletionStatus", "Pending");
        }
    }
}
