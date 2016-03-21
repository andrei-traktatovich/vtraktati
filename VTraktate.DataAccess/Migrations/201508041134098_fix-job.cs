namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixjob : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Jobs", new[] { "Order_Id" });
            RenameColumn(table: "dbo.Jobs", name: "Order_Id", newName: "OrderId");
            AlterColumn("dbo.Jobs", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "OrderId");
            AddForeignKey("dbo.Jobs", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "OrderId", "dbo.Orders");
            DropIndex("dbo.Jobs", new[] { "OrderId" });
            AlterColumn("dbo.Jobs", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.Jobs", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.Jobs", "Order_Id");
            AddForeignKey("dbo.Jobs", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
