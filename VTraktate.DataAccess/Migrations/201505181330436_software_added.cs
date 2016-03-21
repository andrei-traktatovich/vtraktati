namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class software_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProviderSofts",
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
            DropForeignKey("dbo.ProviderSofts", "Provider_Id", "dbo.Providers");
            DropIndex("dbo.ProviderSofts", new[] { "Provider_Id" });
            DropTable("dbo.ProviderSofts");
        }
    }
}
