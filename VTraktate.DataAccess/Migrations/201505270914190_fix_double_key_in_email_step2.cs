namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_double_key_in_email_step2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "ContactPersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Emails", "ContactPersonId");
            AddForeignKey("dbo.Emails", "ContactPersonId", "dbo.People", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "ContactPersonId", "dbo.People");
            DropIndex("dbo.Emails", new[] { "ContactPersonId" });
            DropColumn("dbo.Emails", "ContactPersonId");
        }
    }
}
