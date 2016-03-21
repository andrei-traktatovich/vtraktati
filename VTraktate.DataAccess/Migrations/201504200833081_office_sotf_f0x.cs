namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class office_sotf_f0x : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offices", "OfficeTypeID_Id", "dbo.OfficeTypes");
            DropIndex("dbo.Offices", new[] { "OfficeTypeID_Id" });
            RenameColumn(table: "dbo.Offices", name: "OfficeTypeID_Id", newName: "OfficeTypeId");
            AlterColumn("dbo.Offices", "OfficeTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Offices", "OfficeTypeId");
            AddForeignKey("dbo.Offices", "OfficeTypeId", "dbo.OfficeTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.Offices", "OfficeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Offices", "OfficeType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Offices", "OfficeTypeId", "dbo.OfficeTypes");
            DropIndex("dbo.Offices", new[] { "OfficeTypeId" });
            AlterColumn("dbo.Offices", "OfficeTypeId", c => c.Int());
            RenameColumn(table: "dbo.Offices", name: "OfficeTypeId", newName: "OfficeTypeID_Id");
            CreateIndex("dbo.Offices", "OfficeTypeID_Id");
            AddForeignKey("dbo.Offices", "OfficeTypeID_Id", "dbo.OfficeTypes", "Id");
        }
    }
}
