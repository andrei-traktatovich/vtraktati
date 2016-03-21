namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateGrades : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Grades", "LegacyLanguageName");
            DropColumn("dbo.Grades", "LegacyPrimaryDomainName");
            DropColumn("dbo.Grades", "LegacySecondaryDomainName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Grades", "LegacySecondaryDomainName", c => c.String());
            AddColumn("dbo.Grades", "LegacyPrimaryDomainName", c => c.String());
            AddColumn("dbo.Grades", "LegacyLanguageName", c => c.String());
        }
    }
}
