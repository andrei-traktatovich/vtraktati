namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specify_job : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Initial_Volume_Pages", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Jobs", "Initial_Volume_Chars", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Initial_Volume_Words", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Initial_Pricing_Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Jobs", "Initial_Pricing_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Jobs", "Initial_Pricing_Discount", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Initial_Pricing_DiscountedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Jobs", "LanguageId", c => c.Int());
            AddColumn("dbo.Jobs", "JobTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Document", c => c.String());
            AddColumn("dbo.Jobs", "Domain1Id", c => c.Int());
            AddColumn("dbo.Jobs", "Domain2Id", c => c.Int());
            DropColumn("dbo.Jobs", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Name", c => c.String());
            DropColumn("dbo.Jobs", "Domain2Id");
            DropColumn("dbo.Jobs", "Domain1Id");
            DropColumn("dbo.Jobs", "Document");
            DropColumn("dbo.Jobs", "JobTypeId");
            DropColumn("dbo.Jobs", "LanguageId");
            DropColumn("dbo.Jobs", "Initial_Pricing_DiscountedPrice");
            DropColumn("dbo.Jobs", "Initial_Pricing_Discount");
            DropColumn("dbo.Jobs", "Initial_Pricing_Price");
            DropColumn("dbo.Jobs", "Initial_Pricing_Rate");
            DropColumn("dbo.Jobs", "Initial_Volume_Words");
            DropColumn("dbo.Jobs", "Initial_Volume_Chars");
            DropColumn("dbo.Jobs", "Initial_Volume_Pages");
        }
    }
}
