namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class promotion_to_timestamped : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Promotions", "PromotedById", "dbo.People");
            DropIndex("dbo.Promotions", new[] { "PromotedById" });
            RenameColumn(table: "dbo.Promotions", name: "PromotedById", newName: "CreatedById");
            AddColumn("dbo.Promotions", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Promotions", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.Promotions", "ModifiedById", c => c.Int());
            AlterColumn("dbo.Promotions", "CreatedById", c => c.Int());
            CreateIndex("dbo.Promotions", "CreatedById");
            CreateIndex("dbo.Promotions", "ModifiedById");
            AddForeignKey("dbo.Promotions", "ModifiedById", "dbo.People", "Id");
            AddForeignKey("dbo.Promotions", "CreatedById", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promotions", "CreatedById", "dbo.People");
            DropForeignKey("dbo.Promotions", "ModifiedById", "dbo.People");
            DropIndex("dbo.Promotions", new[] { "ModifiedById" });
            DropIndex("dbo.Promotions", new[] { "CreatedById" });
            AlterColumn("dbo.Promotions", "CreatedById", c => c.Int(nullable: false));
            DropColumn("dbo.Promotions", "ModifiedById");
            DropColumn("dbo.Promotions", "ModifiedDate");
            DropColumn("dbo.Promotions", "CreatedDate");
            RenameColumn(table: "dbo.Promotions", name: "CreatedById", newName: "PromotedById");
            CreateIndex("dbo.Promotions", "PromotedById");
            AddForeignKey("dbo.Promotions", "PromotedById", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
