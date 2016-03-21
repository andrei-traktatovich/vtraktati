namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_address_to_provider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Providers", "City", c => c.String());
            AddColumn("dbo.Providers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Providers", "Address");
            DropColumn("dbo.Providers", "City");
        }
    }
}
