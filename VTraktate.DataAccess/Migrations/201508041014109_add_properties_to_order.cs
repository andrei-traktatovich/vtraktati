namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_properties_to_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Comment", c => c.String());
            AddColumn("dbo.Orders", "TransliterationRequirements", c => c.String());
            AddColumn("dbo.Orders", "PlannedDeliveryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "Document");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Document", c => c.String());
            DropColumn("dbo.Orders", "PlannedDeliveryDate");
            DropColumn("dbo.Orders", "TransliterationRequirements");
            DropColumn("dbo.Orders", "Comment");
        }
    }
}
