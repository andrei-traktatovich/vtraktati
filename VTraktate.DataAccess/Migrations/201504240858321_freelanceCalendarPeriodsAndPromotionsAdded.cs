namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class freelanceCalendarPeriodsAndPromotionsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FreelanceCalendarPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        StatusId = c.Int(nullable: false),
                        ProviderId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.FreelanceCalendarStatus", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.ProviderId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.FreelanceCalendarStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Availability = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PromoteeId = c.Int(nullable: false),
                        PromotedById = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PromotedById, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.PromoteeId, cascadeDelete: true)
                .Index(t => t.PromoteeId)
                .Index(t => t.PromotedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promotions", "PromoteeId", "dbo.Providers");
            DropForeignKey("dbo.Promotions", "PromotedById", "dbo.People");
            DropForeignKey("dbo.FreelanceCalendarPeriods", "StatusId", "dbo.FreelanceCalendarStatus");
            DropForeignKey("dbo.FreelanceCalendarPeriods", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.FreelanceCalendarPeriods", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.FreelanceCalendarPeriods", "CreatedById", "dbo.People");
            DropIndex("dbo.Promotions", new[] { "PromotedById" });
            DropIndex("dbo.Promotions", new[] { "PromoteeId" });
            DropIndex("dbo.FreelanceCalendarPeriods", new[] { "ModifiedById" });
            DropIndex("dbo.FreelanceCalendarPeriods", new[] { "CreatedById" });
            DropIndex("dbo.FreelanceCalendarPeriods", new[] { "ProviderId" });
            DropIndex("dbo.FreelanceCalendarPeriods", new[] { "StatusId" });
            DropTable("dbo.Promotions");
            DropTable("dbo.FreelanceCalendarStatus");
            DropTable("dbo.FreelanceCalendarPeriods");
        }
    }
}
