namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class additionalstuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Name");
        }
    }
}
