namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_providersoft : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProviderSofts", "Provider_Id", "dbo.Providers");
            DropIndex("dbo.ProviderSofts", new[] { "Provider_Id" });
            CreateTable(
                "dbo.ProviderSoftProviders",
                c => new
                    {
                        ProviderSoft_Id = c.Int(nullable: false),
                        Provider_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProviderSoft_Id, t.Provider_Id })
                .ForeignKey("dbo.ProviderSofts", t => t.ProviderSoft_Id, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.Provider_Id, cascadeDelete: true)
                .Index(t => t.ProviderSoft_Id)
                .Index(t => t.Provider_Id);
            
            DropColumn("dbo.ProviderSofts", "Provider_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProviderSofts", "Provider_Id", c => c.Int());
            DropForeignKey("dbo.ProviderSoftProviders", "Provider_Id", "dbo.Providers");
            DropForeignKey("dbo.ProviderSoftProviders", "ProviderSoft_Id", "dbo.ProviderSofts");
            DropIndex("dbo.ProviderSoftProviders", new[] { "Provider_Id" });
            DropIndex("dbo.ProviderSoftProviders", new[] { "ProviderSoft_Id" });
            DropTable("dbo.ProviderSoftProviders");
            CreateIndex("dbo.ProviderSofts", "Provider_Id");
            AddForeignKey("dbo.ProviderSofts", "Provider_Id", "dbo.Providers", "Id");
        }
    }
}
