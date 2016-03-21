namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_double_key_in_email_step1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emails", "PersonId", "dbo.People");
            DropForeignKey("dbo.Emails", "Person_Id", "dbo.People");
            DropIndex("dbo.Emails", new[] { "PersonId" });
            DropIndex("dbo.Emails", new[] { "Person_Id" });
            DropColumn("dbo.Emails", "PersonId");
            DropColumn("dbo.Emails", "Person_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emails", "Person_Id", c => c.Int());
            AddColumn("dbo.Emails", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Emails", "Person_Id");
            CreateIndex("dbo.Emails", "PersonId");
            AddForeignKey("dbo.Emails", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Emails", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
