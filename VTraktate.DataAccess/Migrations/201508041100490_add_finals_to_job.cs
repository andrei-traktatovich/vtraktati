namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_finals_to_job : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Final_Volume_Pages", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Jobs", "Final_Volume_Chars", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Final_Volume_Words", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Final_Pricing_Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Jobs", "Final_Pricing_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Jobs", "Final_Pricing_Discount", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Final_Pricing_DiscountedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Final_Pricing_DiscountedPrice");
            DropColumn("dbo.Jobs", "Final_Pricing_Discount");
            DropColumn("dbo.Jobs", "Final_Pricing_Price");
            DropColumn("dbo.Jobs", "Final_Pricing_Rate");
            DropColumn("dbo.Jobs", "Final_Volume_Words");
            DropColumn("dbo.Jobs", "Final_Volume_Chars");
            DropColumn("dbo.Jobs", "Final_Volume_Pages");
        }
    }
}
