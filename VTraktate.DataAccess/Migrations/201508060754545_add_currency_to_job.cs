namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_currency_to_job : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "CurrencyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "CurrencyId");
            AddForeignKey("dbo.Jobs", "CurrencyId", "dbo.Currencies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.Jobs", new[] { "CurrencyId" });
            DropColumn("dbo.Jobs", "CurrencyId");
        }
    }
}
