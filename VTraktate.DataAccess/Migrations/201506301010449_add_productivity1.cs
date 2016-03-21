namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_productivity1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceLanguageInfoes", "ProductivityMin", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ServiceLanguageInfoes", "ProductivityMax", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceLanguageInfoes", "ProductivityMax");
            DropColumn("dbo.ServiceLanguageInfoes", "ProductivityMin");
        }
    }
}
