namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_reference_to_language_from_domains : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceDomainInfoes", "ServiceLanguageInfo_Id", "dbo.ServiceLanguageInfoes");
            DropIndex("dbo.ServiceDomainInfoes", new[] { "ServiceLanguageInfo_Id" });
            RenameColumn(table: "dbo.ServiceDomainInfoes", name: "ServiceLanguageInfo_Id", newName: "LanguageId");
            AlterColumn("dbo.ServiceDomainInfoes", "LanguageId", c => c.Int(nullable: false));
            CreateIndex("dbo.ServiceDomainInfoes", "LanguageId");
            AddForeignKey("dbo.ServiceDomainInfoes", "LanguageId", "dbo.ServiceLanguageInfoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceDomainInfoes", "LanguageId", "dbo.ServiceLanguageInfoes");
            DropIndex("dbo.ServiceDomainInfoes", new[] { "LanguageId" });
            AlterColumn("dbo.ServiceDomainInfoes", "LanguageId", c => c.Int());
            RenameColumn(table: "dbo.ServiceDomainInfoes", name: "LanguageId", newName: "ServiceLanguageInfo_Id");
            CreateIndex("dbo.ServiceDomainInfoes", "ServiceLanguageInfo_Id");
            AddForeignKey("dbo.ServiceDomainInfoes", "ServiceLanguageInfo_Id", "dbo.ServiceLanguageInfoes", "Id");
        }
    }
}
