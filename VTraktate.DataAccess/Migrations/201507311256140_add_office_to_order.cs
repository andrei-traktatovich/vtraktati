namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_office_to_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OfficeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "OfficeId");
            AddForeignKey("dbo.Orders", "OfficeId", "dbo.Offices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OfficeId", "dbo.Offices");
            DropIndex("dbo.Orders", new[] { "OfficeId" });
            DropColumn("dbo.Orders", "OfficeId");
        }
    }
}
