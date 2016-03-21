namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_calendarPeriods : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FreelanceCalendarPeriods", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Employments", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CalendarPeriods", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.FreelanceCalendarPeriods", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Freelances", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Promotions", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.JobParts", "StartDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobParts", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Promotions", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Freelances", "StartDate", c => c.DateTime());
            AlterColumn("dbo.FreelanceCalendarPeriods", "StartDate", c => c.DateTime());
            AlterColumn("dbo.CalendarPeriods", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Employments", "StartDate", c => c.DateTime());
            DropColumn("dbo.FreelanceCalendarPeriods", "IsDeleted");
        }
    }
}
