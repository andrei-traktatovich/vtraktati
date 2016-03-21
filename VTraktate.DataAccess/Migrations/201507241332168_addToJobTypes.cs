namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addToJobTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobTypes", "LongName", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.JobTypes", "LongName", unique: true, name: "IX_JobTypeLongName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.JobTypes", "IX_JobTypeLongName");
            DropColumn("dbo.JobTypes", "LongName");
        }
    }
}
