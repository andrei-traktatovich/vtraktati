namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changePendingToIsPending : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPartCompletionStatus", "IsPending", c => c.Boolean(nullable: false));
            DropColumn("dbo.JobPartCompletionStatus", "Pending");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobPartCompletionStatus", "Pending", c => c.Boolean(nullable: false));
            DropColumn("dbo.JobPartCompletionStatus", "IsPending");
        }
    }
}
