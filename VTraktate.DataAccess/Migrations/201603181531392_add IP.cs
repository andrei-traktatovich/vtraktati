namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LegalForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Providers", "LegalForm_Id", c => c.Int());
            CreateIndex("dbo.Providers", "LegalForm_Id");
            AddForeignKey("dbo.Providers", "LegalForm_Id", "dbo.LegalForms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Providers", "LegalForm_Id", "dbo.LegalForms");
            DropIndex("dbo.Providers", new[] { "LegalForm_Id" });
            DropColumn("dbo.Providers", "LegalForm_Id");
            DropTable("dbo.LegalForms");
        }
    }
}
