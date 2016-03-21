namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makejobtimestamped : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "CreatedById", c => c.Int());
            AddColumn("dbo.Jobs", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.Jobs", "ModifiedById", c => c.Int());
            CreateIndex("dbo.Jobs", "CreatedById");
            CreateIndex("dbo.Jobs", "ModifiedById");
            AddForeignKey("dbo.Jobs", "CreatedById", "dbo.People", "Id");
            AddForeignKey("dbo.Jobs", "ModifiedById", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.Jobs", "CreatedById", "dbo.People");
            DropIndex("dbo.Jobs", new[] { "ModifiedById" });
            DropIndex("dbo.Jobs", new[] { "CreatedById" });
            DropColumn("dbo.Jobs", "ModifiedById");
            DropColumn("dbo.Jobs", "ModifiedDate");
            DropColumn("dbo.Jobs", "CreatedById");
            DropColumn("dbo.Jobs", "CreatedDate");
        }
    }
}
