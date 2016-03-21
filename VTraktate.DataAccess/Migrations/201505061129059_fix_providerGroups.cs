namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_providerGroups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProviderGroups", "Provider_Id", "dbo.Providers");
            DropIndex("dbo.ProviderGroups", new[] { "Provider_Id" });
            CreateTable(
                "dbo.ProviderProviderGroups",
                c => new
                    {
                        Provider_Id = c.Int(nullable: false),
                        ProviderGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Provider_Id, t.ProviderGroup_Id })
                .ForeignKey("dbo.Providers", t => t.Provider_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProviderGroups", t => t.ProviderGroup_Id, cascadeDelete: true)
                .Index(t => t.Provider_Id)
                .Index(t => t.ProviderGroup_Id);
            
            DropColumn("dbo.ProviderGroups", "Provider_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProviderGroups", "Provider_Id", c => c.Int());
            DropForeignKey("dbo.ProviderProviderGroups", "ProviderGroup_Id", "dbo.ProviderGroups");
            DropForeignKey("dbo.ProviderProviderGroups", "Provider_Id", "dbo.Providers");
            DropIndex("dbo.ProviderProviderGroups", new[] { "ProviderGroup_Id" });
            DropIndex("dbo.ProviderProviderGroups", new[] { "Provider_Id" });
            DropTable("dbo.ProviderProviderGroups");
            CreateIndex("dbo.ProviderGroups", "Provider_Id");
            AddForeignKey("dbo.ProviderGroups", "Provider_Id", "dbo.Providers", "Id");
        }
    }
}
