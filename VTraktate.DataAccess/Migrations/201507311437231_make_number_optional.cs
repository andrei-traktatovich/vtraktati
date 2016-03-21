namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class make_number_optional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Number", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Number", c => c.Int(nullable: false));
        }
    }
}
