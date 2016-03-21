namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_freelance_calendar_statuses_to_availability_statuses : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FreelanceCalendarStatus", newName: "ProviderAvailabilityStatus");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ProviderAvailabilityStatus", newName: "FreelanceCalendarStatus");
        }
    }
}
