namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_bugs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OtherContacts", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.OtherContacts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Phones", "IsDeleted", c => c.Boolean(nullable: false));
            CreateIndex("dbo.OtherContacts", "PersonId");
            AddForeignKey("dbo.OtherContacts", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OtherContacts", "PersonId", "dbo.People");
            DropIndex("dbo.OtherContacts", new[] { "PersonId" });
            DropColumn("dbo.Phones", "IsDeleted");
            DropColumn("dbo.OtherContacts", "IsDeleted");
            DropColumn("dbo.OtherContacts", "PersonId");
        }
    }
}
