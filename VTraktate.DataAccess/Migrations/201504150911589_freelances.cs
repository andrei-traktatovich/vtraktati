namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class freelances : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Freelances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FreelanceStatusID = c.Int(nullable: false),
                        ProviderID = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Comment = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.FreelanceStatus", t => t.FreelanceStatusID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.Providers", t => t.ProviderID, cascadeDelete: true)
                .Index(t => t.FreelanceStatusID)
                .Index(t => t.ProviderID)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.FreelanceStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Freelances", "ProviderID", "dbo.Providers");
            DropForeignKey("dbo.Freelances", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.Freelances", "FreelanceStatusID", "dbo.FreelanceStatus");
            DropForeignKey("dbo.Freelances", "CreatedById", "dbo.People");
            DropIndex("dbo.Freelances", new[] { "ModifiedById" });
            DropIndex("dbo.Freelances", new[] { "CreatedById" });
            DropIndex("dbo.Freelances", new[] { "ProviderID" });
            DropIndex("dbo.Freelances", new[] { "FreelanceStatusID" });
            DropTable("dbo.FreelanceStatus");
            DropTable("dbo.Freelances");
        }
    }
}
