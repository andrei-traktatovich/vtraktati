namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class person_and_provider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "ProviderId", c => c.Int());
            //AddColumn("dbo.People", "Provider_Id", c => c.Int());
            CreateIndex("dbo.People", "ProviderId");
            //CreateIndex("dbo.People", "Provider_Id");
            //AddForeignKey("dbo.People", "Provider_Id", "dbo.Providers", "Id");
            AddForeignKey("dbo.People", "ProviderId", "dbo.Providers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "ProviderId", "dbo.Providers");
            //DropForeignKey("dbo.People", "Provider_Id", "dbo.Providers");
            //DropIndex("dbo.People", new[] { "Provider_Id" });
            DropIndex("dbo.People", new[] { "ProviderId" });
            //DropColumn("dbo.People", "Provider_Id");
            DropColumn("dbo.People", "ProviderId");
        }
    }
}
