namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class document_added_to_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Document", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Document");
        }
    }
}
