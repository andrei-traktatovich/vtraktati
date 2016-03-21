namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_provider_id : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.People", new[] { "Provider_Id" });
            //DropColumn("dbo.People", "ProviderId");
            //RenameColumn(table: "dbo.People", name: "Provider_Id", newName: "ProviderId");
        }
        
        public override void Down()
        {
            //RenameColumn(table: "dbo.People", name: "ProviderId", newName: "Provider_Id");
            //AddColumn("dbo.People", "ProviderId", c => c.Int());
            //CreateIndex("dbo.People", "Provider_Id");
        }
    }
}
