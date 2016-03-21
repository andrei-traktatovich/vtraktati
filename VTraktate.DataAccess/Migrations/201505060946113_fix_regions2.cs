namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_regions2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Providers", "RegionId", "dbo.Regions");
            DropIndex("dbo.Providers", new[] { "RegionId" });
            AlterColumn("dbo.Providers", "RegionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Providers", "RegionId");
            AddForeignKey("dbo.Providers", "RegionId", "dbo.Regions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Providers", "RegionId", "dbo.Regions");
            DropIndex("dbo.Providers", new[] { "RegionId" });
            AlterColumn("dbo.Providers", "RegionId", c => c.Int());
            CreateIndex("dbo.Providers", "RegionId");
            AddForeignKey("dbo.Providers", "RegionId", "dbo.Regions", "Id");
        }
    }
}
