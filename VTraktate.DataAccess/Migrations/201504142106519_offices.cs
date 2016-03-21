namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class offices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProviderID = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Providers", "OfficeID", c => c.Int());
            AddColumn("dbo.Providers", "Office_Id", c => c.Int());
            CreateIndex("dbo.Providers", "Office_Id");
            AddForeignKey("dbo.Providers", "Office_Id", "dbo.Offices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Providers", "Office_Id", "dbo.Offices");
            DropIndex("dbo.Providers", new[] { "Office_Id" });
            DropColumn("dbo.Providers", "Office_Id");
            DropColumn("dbo.Providers", "OfficeID");
            DropTable("dbo.Offices");
        }
    }
}
