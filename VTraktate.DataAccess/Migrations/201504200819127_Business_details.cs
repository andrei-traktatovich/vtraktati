namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Business_details : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LegalEntities", "Details_INN", c => c.String());
            AddColumn("dbo.LegalEntities", "Details_KPP", c => c.String());
            AddColumn("dbo.LegalEntities", "Details_LegalAddress", c => c.String());
            AddColumn("dbo.LegalEntities", "Details_ActualAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LegalEntities", "Details_ActualAddress");
            DropColumn("dbo.LegalEntities", "Details_LegalAddress");
            DropColumn("dbo.LegalEntities", "Details_KPP");
            DropColumn("dbo.LegalEntities", "Details_INN");
        }
    }
}
