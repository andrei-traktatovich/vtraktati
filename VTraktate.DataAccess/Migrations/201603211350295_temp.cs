namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class temp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Providers", "LegalForm_Id", "dbo.LegalForms");
            DropIndex("dbo.Providers", new[] { "LegalForm_Id" });
            RenameColumn(table: "dbo.Providers", name: "LegalForm_Id", newName: "LegalFormId");
            AddColumn("dbo.Providers", "WorksNightly", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Providers", "LegalFormId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Providers", "LegalFormId");
            AddForeignKey("dbo.Providers", "LegalFormId", "dbo.LegalForms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Providers", "LegalFormId", "dbo.LegalForms");
            DropIndex("dbo.Providers", new[] { "LegalFormId" });
            AlterColumn("dbo.Providers", "LegalFormId", c => c.Int());
            DropColumn("dbo.Providers", "WorksNightly");
            RenameColumn(table: "dbo.Providers", name: "LegalFormId", newName: "LegalForm_Id");
            CreateIndex("dbo.Providers", "LegalForm_Id");
            AddForeignKey("dbo.Providers", "LegalForm_Id", "dbo.LegalForms", "Id");
        }
    }
}
