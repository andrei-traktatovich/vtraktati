namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class make_jobPartITimeStamped : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobParts", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.JobParts", "CreatedById", c => c.Int());
            AddColumn("dbo.JobParts", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.JobParts", "ModifiedById", c => c.Int());
            CreateIndex("dbo.JobParts", "CreatedById");
            CreateIndex("dbo.JobParts", "ModifiedById");
            AddForeignKey("dbo.JobParts", "CreatedById", "dbo.People", "Id");
            AddForeignKey("dbo.JobParts", "ModifiedById", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobParts", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.JobParts", "CreatedById", "dbo.People");
            DropIndex("dbo.JobParts", new[] { "ModifiedById" });
            DropIndex("dbo.JobParts", new[] { "CreatedById" });
            DropColumn("dbo.JobParts", "ModifiedById");
            DropColumn("dbo.JobParts", "ModifiedDate");
            DropColumn("dbo.JobParts", "CreatedById");
            DropColumn("dbo.JobParts", "CreatedDate");
        }
    }
}
