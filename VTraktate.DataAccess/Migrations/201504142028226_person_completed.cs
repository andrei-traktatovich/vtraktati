namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class person_completed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.People", "Comment", c => c.String());
            AddColumn("dbo.People", "BirthDate", c => c.DateTime());
            AddColumn("dbo.People", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.People", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.People", "CreatedBy_Id", c => c.Int(nullable: false, defaultValue: 1 ));
            AddColumn("dbo.People", "ModifiedBy_Id", c => c.Int());
            CreateIndex("dbo.People", "CreatedBy_Id");
            CreateIndex("dbo.People", "ModifiedBy_Id");
            AddForeignKey("dbo.People", "CreatedBy_Id", "dbo.People", "Id");
            AddForeignKey("dbo.People", "ModifiedBy_Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "ModifiedBy_Id", "dbo.People");
            DropForeignKey("dbo.People", "CreatedBy_Id", "dbo.People");
            DropIndex("dbo.People", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.People", new[] { "CreatedBy_Id" });
            DropColumn("dbo.People", "ModifiedBy_Id");
            DropColumn("dbo.People", "CreatedBy_Id");
            DropColumn("dbo.People", "ModifiedDate");
            DropColumn("dbo.People", "CreatedDate");
            DropColumn("dbo.People", "BirthDate");
            DropColumn("dbo.People", "Comment");
            DropColumn("dbo.People", "IsDeleted");
        }
    }
}
