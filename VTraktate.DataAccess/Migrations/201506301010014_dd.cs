namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ServiceLanguageInfoes", "ProductivityMin");
            DropColumn("dbo.ServiceLanguageInfoes", "ProductivityMax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceLanguageInfoes", "ProductivityMax", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ServiceLanguageInfoes", "ProductivityMin", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
