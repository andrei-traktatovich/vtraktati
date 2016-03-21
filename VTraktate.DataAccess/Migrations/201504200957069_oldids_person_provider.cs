namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oldids_person_provider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Old_Id", c => c.Int());
            AddColumn("dbo.Providers", "Old_Id", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Providers", "Old_Id");
            DropColumn("dbo.People", "Old_Id");
        }
    }
}
