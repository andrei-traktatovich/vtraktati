namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class calendar_period_confirmed_by : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarPeriods", "Confirmed", c => c.Boolean());
            AddColumn("dbo.CalendarPeriods", "ConfirmedById", c => c.Int());
            CreateIndex("dbo.CalendarPeriods", "ConfirmedById");
            AddForeignKey("dbo.CalendarPeriods", "ConfirmedById", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CalendarPeriods", "ConfirmedById", "dbo.People");
            DropIndex("dbo.CalendarPeriods", new[] { "ConfirmedById" });
            DropColumn("dbo.CalendarPeriods", "ConfirmedById");
            DropColumn("dbo.CalendarPeriods", "Confirmed");
        }
    }
}
