namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relate_job_to_jobTypeAndLanguageAndDomains : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Jobs", "LanguageId");
            CreateIndex("dbo.Jobs", "JobTypeId");
            CreateIndex("dbo.Jobs", "Domain1Id");
            CreateIndex("dbo.Jobs", "Domain2Id");
            AddForeignKey("dbo.Jobs", "Domain1Id", "dbo.TranslationDomains", "Id");
            AddForeignKey("dbo.Jobs", "Domain2Id", "dbo.TranslationDomains", "Id");
            AddForeignKey("dbo.Jobs", "JobTypeId", "dbo.JobTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "LanguageId", "dbo.LanguagePairs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "LanguageId", "dbo.LanguagePairs");
            DropForeignKey("dbo.Jobs", "JobTypeId", "dbo.JobTypes");
            DropForeignKey("dbo.Jobs", "Domain2Id", "dbo.TranslationDomains");
            DropForeignKey("dbo.Jobs", "Domain1Id", "dbo.TranslationDomains");
            DropIndex("dbo.Jobs", new[] { "Domain2Id" });
            DropIndex("dbo.Jobs", new[] { "Domain1Id" });
            DropIndex("dbo.Jobs", new[] { "JobTypeId" });
            DropIndex("dbo.Jobs", new[] { "LanguageId" });
        }
    }
}
