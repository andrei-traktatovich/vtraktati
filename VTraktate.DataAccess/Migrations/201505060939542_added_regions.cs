namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_regions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Providers", "RegionId", c => c.Int());
            CreateIndex("dbo.Providers", "RegionId");
            AddForeignKey("dbo.Providers", "RegionId", "dbo.Regions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Providers", "RegionId", "dbo.Regions");
            DropIndex("dbo.Providers", new[] { "RegionId" });
            DropColumn("dbo.Providers", "RegionId");
            DropTable("dbo.Regions");
        }
    }
}
