namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class connectGradesToProviders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "ServiceGradedId", c => c.Int());
            AddColumn("dbo.Grades", "ServiceLanguageInfoGradedId", c => c.Int());
            AddColumn("dbo.Grades", "PrimaryDomainGradedId", c => c.Int());
            AddColumn("dbo.Grades", "SecondaryDomainGradedId", c => c.Int());
            AddColumn("dbo.Grades", "ServiceDomainInfo_Id", c => c.Int());
            CreateIndex("dbo.Grades", "ServiceGradedId");
            CreateIndex("dbo.Grades", "ServiceLanguageInfoGradedId");
            CreateIndex("dbo.Grades", "PrimaryDomainGradedId");
            CreateIndex("dbo.Grades", "SecondaryDomainGradedId");
            CreateIndex("dbo.Grades", "ServiceDomainInfo_Id");
            AddForeignKey("dbo.Grades", "PrimaryDomainGradedId", "dbo.ServiceDomainInfoes", "Id");
            AddForeignKey("dbo.Grades", "SecondaryDomainGradedId", "dbo.ServiceDomainInfoes", "Id");
            AddForeignKey("dbo.Grades", "ServiceGradedId", "dbo.Services", "Id");
            AddForeignKey("dbo.Grades", "ServiceLanguageInfoGradedId", "dbo.ServiceLanguageInfoes", "Id");
            AddForeignKey("dbo.Grades", "ServiceDomainInfo_Id", "dbo.ServiceDomainInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "ServiceDomainInfo_Id", "dbo.ServiceDomainInfoes");
            DropForeignKey("dbo.Grades", "ServiceLanguageInfoGradedId", "dbo.ServiceLanguageInfoes");
            DropForeignKey("dbo.Grades", "ServiceGradedId", "dbo.Services");
            DropForeignKey("dbo.Grades", "SecondaryDomainGradedId", "dbo.ServiceDomainInfoes");
            DropForeignKey("dbo.Grades", "PrimaryDomainGradedId", "dbo.ServiceDomainInfoes");
            DropIndex("dbo.Grades", new[] { "ServiceDomainInfo_Id" });
            DropIndex("dbo.Grades", new[] { "SecondaryDomainGradedId" });
            DropIndex("dbo.Grades", new[] { "PrimaryDomainGradedId" });
            DropIndex("dbo.Grades", new[] { "ServiceLanguageInfoGradedId" });
            DropIndex("dbo.Grades", new[] { "ServiceGradedId" });
            DropColumn("dbo.Grades", "ServiceDomainInfo_Id");
            DropColumn("dbo.Grades", "SecondaryDomainGradedId");
            DropColumn("dbo.Grades", "PrimaryDomainGradedId");
            DropColumn("dbo.Grades", "ServiceLanguageInfoGradedId");
            DropColumn("dbo.Grades", "ServiceGradedId");
        }
    }
}
