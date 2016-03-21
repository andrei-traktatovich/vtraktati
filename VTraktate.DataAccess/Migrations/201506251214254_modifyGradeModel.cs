namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyGradeModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "ServiceTypeId", c => c.Int());
            AddColumn("dbo.Grades", "LanguagePairId", c => c.Int());
            AddColumn("dbo.Grades", "PrimaryDomainId", c => c.Int());
            AddColumn("dbo.Grades", "SecondaryDomainId", c => c.Int());
            CreateIndex("dbo.Grades", "ServiceTypeId");
            CreateIndex("dbo.Grades", "LanguagePairId");
            CreateIndex("dbo.Grades", "PrimaryDomainId");
            CreateIndex("dbo.Grades", "SecondaryDomainId");
            AddForeignKey("dbo.Grades", "LanguagePairId", "dbo.LanguagePairs", "Id");
            AddForeignKey("dbo.Grades", "PrimaryDomainId", "dbo.TranslationDomains", "Id");
            AddForeignKey("dbo.Grades", "SecondaryDomainId", "dbo.TranslationDomains", "Id");
            AddForeignKey("dbo.Grades", "ServiceTypeId", "dbo.ServiceTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "ServiceTypeId", "dbo.ServiceTypes");
            DropForeignKey("dbo.Grades", "SecondaryDomainId", "dbo.TranslationDomains");
            DropForeignKey("dbo.Grades", "PrimaryDomainId", "dbo.TranslationDomains");
            DropForeignKey("dbo.Grades", "LanguagePairId", "dbo.LanguagePairs");
            DropIndex("dbo.Grades", new[] { "SecondaryDomainId" });
            DropIndex("dbo.Grades", new[] { "PrimaryDomainId" });
            DropIndex("dbo.Grades", new[] { "LanguagePairId" });
            DropIndex("dbo.Grades", new[] { "ServiceTypeId" });
            DropColumn("dbo.Grades", "SecondaryDomainId");
            DropColumn("dbo.Grades", "PrimaryDomainId");
            DropColumn("dbo.Grades", "LanguagePairId");
            DropColumn("dbo.Grades", "ServiceTypeId");
        }
    }
}
