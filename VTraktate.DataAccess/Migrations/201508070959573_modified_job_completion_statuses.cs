namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modified_job_completion_statuses : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.JobCompletionStatus", name: "IX_Name", newName: "IX_JobCompletionStatusName");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.JobCompletionStatus", name: "IX_JobCompletionStatusName", newName: "IX_Name");
        }
    }
}
