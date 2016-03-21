namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify_job_completion_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobCompletionStatus", "FinalVolumeRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobCompletionStatus", "FinalVolumeRequired");
        }
    }
}
