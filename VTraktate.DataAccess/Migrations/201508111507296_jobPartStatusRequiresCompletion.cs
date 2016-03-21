namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobPartStatusRequiresCompletion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPartCompletionStatus", "RequiresCompletion", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobPartCompletionStatus", "RequiresCompletion");
        }
    }
}
