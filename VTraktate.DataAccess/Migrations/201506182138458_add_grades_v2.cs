namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_grades_v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "LegacyJobName", c => c.String());
            AddColumn("dbo.Grades", "LegacyLanguageName", c => c.String());
            AddColumn("dbo.Grades", "LegacyPrimaryDomainName", c => c.String());
            AddColumn("dbo.Grades", "LegacySecondaryDomainName", c => c.String());
            DropColumn("dbo.Grades", "JobName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Grades", "JobName", c => c.String());
            DropColumn("dbo.Grades", "LegacySecondaryDomainName");
            DropColumn("dbo.Grades", "LegacyPrimaryDomainName");
            DropColumn("dbo.Grades", "LegacyLanguageName");
            DropColumn("dbo.Grades", "LegacyJobName");
        }
    }
}
