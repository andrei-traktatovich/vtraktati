namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_soft_delete_to_freelance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Freelances", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Freelances", "IsDeleted");
        }
    }
}
