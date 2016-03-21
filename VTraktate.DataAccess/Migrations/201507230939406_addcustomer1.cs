namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomer1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "LongName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "ShortName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Code", c => c.String());
            AlterColumn("dbo.Customers", "ShortName", c => c.String());
            AlterColumn("dbo.Customers", "LongName", c => c.String());
        }
    }
}
