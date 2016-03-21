namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_jobs_to_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Order_Id", c => c.Int());
            CreateIndex("dbo.Jobs", "Order_Id");
            AddForeignKey("dbo.Jobs", "Order_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Jobs", new[] { "Order_Id" });
            DropColumn("dbo.Jobs", "Order_Id");
        }
    }
}
