namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_office_provider : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Providers", "OfficeID");
            DropColumn("dbo.Offices", "ProviderID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Offices", "ProviderID", c => c.Int());
            AddColumn("dbo.Providers", "OfficeID", c => c.Int());
        }
    }
}
