namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_delegated_office_toJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "DelegatedToOfficeId", c => c.Int());
            CreateIndex("dbo.Jobs", "DelegatedToOfficeId");
            AddForeignKey("dbo.Jobs", "DelegatedToOfficeId", "dbo.Offices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "DelegatedToOfficeId", "dbo.Offices");
            DropIndex("dbo.Jobs", new[] { "DelegatedToOfficeId" });
            DropColumn("dbo.Jobs", "DelegatedToOfficeId");
        }
    }
}
