namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class legacyIDs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offices", "LegacyId", c => c.Int());
            AddColumn("dbo.Titles", "LegacyId", c => c.Int());
            AddColumn("dbo.Services", "LegacyId", c => c.Int());
            AddColumn("dbo.TranslationDomains", "LegacyId", c => c.Int());
            AddColumn("dbo.LanguagePairs", "LegacyId", c => c.Int());
            AddColumn("dbo.ServiceTypes", "LegacyId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceTypes", "LegacyId");
            DropColumn("dbo.LanguagePairs", "LegacyId");
            DropColumn("dbo.TranslationDomains", "LegacyId");
            DropColumn("dbo.Services", "LegacyId");
            DropColumn("dbo.Titles", "LegacyId");
            DropColumn("dbo.Offices", "LegacyId");
        }
    }
}
