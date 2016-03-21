namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class grades_add_style : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "Error_Style", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grades", "Error_Style");
        }
    }
}
