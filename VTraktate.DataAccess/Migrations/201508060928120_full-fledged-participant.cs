namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fullfledgedparticipant : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Jobs", name: "JobCompletionStatusId", newName: "StatusId");
            RenameIndex(table: "dbo.Jobs", name: "IX_JobCompletionStatusId", newName: "IX_StatusId");
            AddColumn("dbo.JobParts", "JobTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.JobParts", "LanguageId", c => c.Int());
            AddColumn("dbo.JobParts", "Initial_Volume_Pages", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.JobParts", "Initial_Volume_Chars", c => c.Int());
            AddColumn("dbo.JobParts", "Initial_Volume_Words", c => c.Int());
            AddColumn("dbo.JobParts", "Initial_Pricing_Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.JobParts", "Initial_Pricing_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.JobParts", "Final_Volume_Pages", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.JobParts", "Final_Volume_Chars", c => c.Int());
            AddColumn("dbo.JobParts", "Final_Volume_Words", c => c.Int());
            AddColumn("dbo.JobParts", "Final_Pricing_Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.JobParts", "Final_Pricing_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.JobParts", "CurrencyId", c => c.Int(nullable: false));
            AddColumn("dbo.JobParts", "UOMId", c => c.Int(nullable: false));
            CreateIndex("dbo.JobParts", "JobTypeId");
            CreateIndex("dbo.JobParts", "LanguageId");
            CreateIndex("dbo.JobParts", "CurrencyId");
            CreateIndex("dbo.JobParts", "UOMId");
            AddForeignKey("dbo.JobParts", "CurrencyId", "dbo.Currencies", "Id");
            AddForeignKey("dbo.JobParts", "JobTypeId", "dbo.JobTypes", "Id");
            AddForeignKey("dbo.JobParts", "LanguageId", "dbo.LanguagePairs", "Id");
            AddForeignKey("dbo.JobParts", "UOMId", "dbo.ServiceUOMs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobParts", "UOMId", "dbo.ServiceUOMs");
            DropForeignKey("dbo.JobParts", "LanguageId", "dbo.LanguagePairs");
            DropForeignKey("dbo.JobParts", "JobTypeId", "dbo.JobTypes");
            DropForeignKey("dbo.JobParts", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.JobParts", new[] { "UOMId" });
            DropIndex("dbo.JobParts", new[] { "CurrencyId" });
            DropIndex("dbo.JobParts", new[] { "LanguageId" });
            DropIndex("dbo.JobParts", new[] { "JobTypeId" });
            DropColumn("dbo.JobParts", "UOMId");
            DropColumn("dbo.JobParts", "CurrencyId");
            DropColumn("dbo.JobParts", "Final_Pricing_Price");
            DropColumn("dbo.JobParts", "Final_Pricing_Rate");
            DropColumn("dbo.JobParts", "Final_Volume_Words");
            DropColumn("dbo.JobParts", "Final_Volume_Chars");
            DropColumn("dbo.JobParts", "Final_Volume_Pages");
            DropColumn("dbo.JobParts", "Initial_Pricing_Price");
            DropColumn("dbo.JobParts", "Initial_Pricing_Rate");
            DropColumn("dbo.JobParts", "Initial_Volume_Words");
            DropColumn("dbo.JobParts", "Initial_Volume_Chars");
            DropColumn("dbo.JobParts", "Initial_Volume_Pages");
            DropColumn("dbo.JobParts", "LanguageId");
            DropColumn("dbo.JobParts", "JobTypeId");
            RenameIndex(table: "dbo.Jobs", name: "IX_StatusId", newName: "IX_JobCompletionStatusId");
            RenameColumn(table: "dbo.Jobs", name: "StatusId", newName: "JobCompletionStatusId");
        }
    }
}
