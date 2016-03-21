namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fucking_elephant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobParts", "Initial_Pricing_Discount", c => c.Int(nullable: false));
            AddColumn("dbo.JobParts", "Initial_Pricing_DiscountedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.JobParts", "Final_Pricing_Discount", c => c.Int(nullable: false));
            AddColumn("dbo.JobParts", "Final_Pricing_DiscountedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobParts", "Final_Pricing_DiscountedPrice");
            DropColumn("dbo.JobParts", "Final_Pricing_Discount");
            DropColumn("dbo.JobParts", "Initial_Pricing_DiscountedPrice");
            DropColumn("dbo.JobParts", "Initial_Pricing_Discount");
        }
    }
}
