namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_otherContacts_reverseProperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OtherContacts", "Person_Id", "dbo.People");
            DropIndex("dbo.OtherContacts", new[] { "PersonId" });
            DropIndex("dbo.OtherContacts", new[] { "Person_Id" });
            DropColumn("dbo.OtherContacts", "PersonId");
            RenameColumn(table: "dbo.OtherContacts", name: "Person_Id", newName: "PersonId");
            AlterColumn("dbo.OtherContacts", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.OtherContacts", "PersonId");
            AddForeignKey("dbo.OtherContacts", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OtherContacts", "PersonId", "dbo.People");
            DropIndex("dbo.OtherContacts", new[] { "PersonId" });
            AlterColumn("dbo.OtherContacts", "PersonId", c => c.Int());
            RenameColumn(table: "dbo.OtherContacts", name: "PersonId", newName: "Person_Id");
            AddColumn("dbo.OtherContacts", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.OtherContacts", "Person_Id");
            CreateIndex("dbo.OtherContacts", "PersonId");
            AddForeignKey("dbo.OtherContacts", "Person_Id", "dbo.People", "Id");
        }
    }
}
