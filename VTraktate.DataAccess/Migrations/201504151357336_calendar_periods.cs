namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class calendar_periods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        StatusId = c.Int(nullable: false),
                        OfficeId = c.Int(nullable: false),
                        ProviderId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                        Employment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.Offices", t => t.OfficeId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeCalendarStatus", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Employments", t => t.Employment_Id)
                .Index(t => t.StatusId)
                .Index(t => t.OfficeId)
                .Index(t => t.ProviderId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.Employment_Id);
            
            CreateTable(
                "dbo.EmployeeCalendarStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Emails", "Person_Id", c => c.Int());
            CreateIndex("dbo.Emails", "Person_Id");
            AddForeignKey("dbo.Emails", "Person_Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CalendarPeriods", "Employment_Id", "dbo.Employments");
            DropForeignKey("dbo.CalendarPeriods", "StatusId", "dbo.EmployeeCalendarStatus");
            DropForeignKey("dbo.CalendarPeriods", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.CalendarPeriods", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.CalendarPeriods", "OfficeId", "dbo.Offices");
            DropForeignKey("dbo.CalendarPeriods", "CreatedById", "dbo.People");
            DropForeignKey("dbo.Emails", "Person_Id", "dbo.People");
            DropIndex("dbo.CalendarPeriods", new[] { "Employment_Id" });
            DropIndex("dbo.CalendarPeriods", new[] { "ModifiedById" });
            DropIndex("dbo.CalendarPeriods", new[] { "CreatedById" });
            DropIndex("dbo.CalendarPeriods", new[] { "ProviderId" });
            DropIndex("dbo.CalendarPeriods", new[] { "OfficeId" });
            DropIndex("dbo.CalendarPeriods", new[] { "StatusId" });
            DropIndex("dbo.Emails", new[] { "Person_Id" });
            DropColumn("dbo.Emails", "Person_Id");
            DropTable("dbo.EmployeeCalendarStatus");
            DropTable("dbo.CalendarPeriods");
        }
    }
}
