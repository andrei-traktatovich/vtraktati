namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobPart_job_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobPartCompletionStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProviderId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.JobPartCompletionStatus", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.ProviderId)
                .Index(t => t.StatusId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobParts", "StatusId", "dbo.JobPartCompletionStatus");
            DropForeignKey("dbo.JobParts", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.JobParts", "JobId", "dbo.Jobs");
            DropIndex("dbo.JobParts", new[] { "JobId" });
            DropIndex("dbo.JobParts", new[] { "StatusId" });
            DropIndex("dbo.JobParts", new[] { "ProviderId" });
            DropTable("dbo.Jobs");
            DropTable("dbo.JobParts");
            DropTable("dbo.JobPartCompletionStatus");
        }
    }
}
