namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class offices_and_office_types_and_legalEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LegalEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OfficeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Offices", "OfficeType", c => c.Int(nullable: false));
            AddColumn("dbo.Offices", "LegalEntityID", c => c.Int(nullable: false));
            AddColumn("dbo.Offices", "SharedEmployees", c => c.Boolean(nullable: false));
            AddColumn("dbo.Offices", "OfficeTypeID_Id", c => c.Int());
            CreateIndex("dbo.Offices", "LegalEntityID");
            CreateIndex("dbo.Offices", "OfficeTypeID_Id");
            AddForeignKey("dbo.Offices", "LegalEntityID", "dbo.LegalEntities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Offices", "OfficeTypeID_Id", "dbo.OfficeTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offices", "OfficeTypeID_Id", "dbo.OfficeTypes");
            DropForeignKey("dbo.Offices", "LegalEntityID", "dbo.LegalEntities");
            DropIndex("dbo.Offices", new[] { "OfficeTypeID_Id" });
            DropIndex("dbo.Offices", new[] { "LegalEntityID" });
            DropColumn("dbo.Offices", "OfficeTypeID_Id");
            DropColumn("dbo.Offices", "SharedEmployees");
            DropColumn("dbo.Offices", "LegalEntityID");
            DropColumn("dbo.Offices", "OfficeType");
            DropTable("dbo.OfficeTypes");
            DropTable("dbo.LegalEntities");
        }
    }
}
