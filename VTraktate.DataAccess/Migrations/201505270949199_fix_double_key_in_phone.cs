namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_double_key_in_phone : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phones", "PersonId", "dbo.People");
            DropForeignKey("dbo.Phones", "Person_Id", "dbo.People");
            DropIndex("dbo.Phones", new[] { "PersonId" });
            DropIndex("dbo.Phones", new[] { "Person_Id" });
            RenameColumn(table: "dbo.Phones", name: "Person_Id", newName: "ContactPersonId");
            AlterColumn("dbo.Phones", "ContactPersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Phones", "ContactPersonId");
            AddForeignKey("dbo.Phones", "ContactPersonId", "dbo.People", "Id", cascadeDelete: true);
            DropColumn("dbo.Phones", "PersonId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Phones", "PersonId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Phones", "ContactPersonId", "dbo.People");
            DropIndex("dbo.Phones", new[] { "ContactPersonId" });
            AlterColumn("dbo.Phones", "ContactPersonId", c => c.Int());
            RenameColumn(table: "dbo.Phones", name: "ContactPersonId", newName: "Person_Id");
            CreateIndex("dbo.Phones", "Person_Id");
            CreateIndex("dbo.Phones", "PersonId");
            AddForeignKey("dbo.Phones", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Phones", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
