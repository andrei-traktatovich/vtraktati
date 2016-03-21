namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer_unique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "LongName", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Customers", "ShortName", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Customers", "Code", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Customers", "ShortName", unique: true, name: "IX_CustomerShortName");
            CreateIndex("dbo.Customers", "Code", unique: true, name: "IX_CustomerCode");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", "IX_CustomerCode");
            DropIndex("dbo.Customers", "IX_CustomerShortName");
            AlterColumn("dbo.Customers", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "ShortName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "LongName", c => c.String(nullable: false));
        }
    }
}
