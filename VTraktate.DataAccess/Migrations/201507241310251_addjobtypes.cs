namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addjobtypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ServiceTypeId = c.Int(nullable: false),
                        UomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeId, cascadeDelete: true)
                .ForeignKey("dbo.JobUOMs", t => t.UomId, cascadeDelete: true)
                .Index(t => t.Name, unique: true, name: "IX_JobTypeName")
                .Index(t => t.ServiceTypeId)
                .Index(t => t.UomId);
            
            CreateTable(
                "dbo.JobUOMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_JobUOMName");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobTypes", "UomId", "dbo.JobUOMs");
            DropForeignKey("dbo.JobTypes", "ServiceTypeId", "dbo.ServiceTypes");
            DropIndex("dbo.JobUOMs", "IX_JobUOMName");
            DropIndex("dbo.JobTypes", new[] { "UomId" });
            DropIndex("dbo.JobTypes", new[] { "ServiceTypeId" });
            DropIndex("dbo.JobTypes", "IX_JobTypeName");
            DropTable("dbo.JobUOMs");
            DropTable("dbo.JobTypes");
        }
    }
}
