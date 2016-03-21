namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "IsDefaultContactPerson", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "ContactPerson_Comment");
            DropColumn("dbo.Orders", "ContactPerson_FullName");
            DropColumn("dbo.Orders", "ContactPerson_Phone");
            DropColumn("dbo.Orders", "ContactPerson_Ext");
            DropColumn("dbo.Orders", "ContactPerson_Email");
            DropColumn("dbo.Orders", "ContactPerson_IsDefault");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ContactPerson_IsDefault", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "ContactPerson_Email", c => c.String());
            AddColumn("dbo.Orders", "ContactPerson_Ext", c => c.String());
            AddColumn("dbo.Orders", "ContactPerson_Phone", c => c.String());
            AddColumn("dbo.Orders", "ContactPerson_FullName", c => c.String());
            AddColumn("dbo.Orders", "ContactPerson_Comment", c => c.String());
            DropColumn("dbo.People", "IsDefaultContactPerson");
        }
    }
}
