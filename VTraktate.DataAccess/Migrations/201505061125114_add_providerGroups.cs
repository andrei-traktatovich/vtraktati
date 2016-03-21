namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_providerGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProviderGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Provider_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.Provider_Id)
                .Index(t => t.Provider_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProviderGroups", "Provider_Id", "dbo.Providers");
            DropIndex("dbo.ProviderGroups", new[] { "Provider_Id" });
            DropTable("dbo.ProviderGroups");
        }
    }
}
