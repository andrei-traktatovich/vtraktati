namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_uom_to_job : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobParts", "UOMId", "dbo.ServiceUOMs");
            AddColumn("dbo.Jobs", "UOMId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "UOMId");
            AddForeignKey("dbo.Jobs", "UOMId", "dbo.JobUOMs", "Id");
            AddForeignKey("dbo.JobParts", "UOMId", "dbo.ServiceUOMs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobParts", "UOMId", "dbo.ServiceUOMs");
            DropForeignKey("dbo.Jobs", "UOMId", "dbo.JobUOMs");
            DropIndex("dbo.Jobs", new[] { "UOMId" });
            DropColumn("dbo.Jobs", "UOMId");
            AddForeignKey("dbo.JobParts", "UOMId", "dbo.ServiceUOMs", "Id", cascadeDelete: true);
        }
    }
}
