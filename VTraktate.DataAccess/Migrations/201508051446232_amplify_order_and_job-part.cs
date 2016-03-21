namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amplify_order_and_jobpart : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", "IX_OrderName");
            CreateTable(
                "dbo.JobCompletionStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            AddColumn("dbo.Jobs", "JobCompletionStatusId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "CompletionDate", c => c.DateTime());
            AddColumn("dbo.Jobs", "EndDate", c => c.DateTime());
            AddColumn("dbo.Jobs", "ParentJobPart_Id", c => c.Int());
            AddColumn("dbo.JobParts", "CompletionDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "Name", c => c.String());
            CreateIndex("dbo.Jobs", "JobCompletionStatusId");
            CreateIndex("dbo.Jobs", "ParentJobPart_Id");
            AddForeignKey("dbo.Jobs", "ParentJobPart_Id", "dbo.JobParts", "Id");
            AddForeignKey("dbo.Jobs", "JobCompletionStatusId", "dbo.JobCompletionStatus", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "PlannedDeliveryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PlannedDeliveryDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Jobs", "JobCompletionStatusId", "dbo.JobCompletionStatus");
            DropForeignKey("dbo.Jobs", "ParentJobPart_Id", "dbo.JobParts");
            DropIndex("dbo.JobCompletionStatus", new[] { "Name" });
            DropIndex("dbo.Jobs", new[] { "ParentJobPart_Id" });
            DropIndex("dbo.Jobs", new[] { "JobCompletionStatusId" });
            AlterColumn("dbo.Orders", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.JobParts", "CompletionDate");
            DropColumn("dbo.Jobs", "ParentJobPart_Id");
            DropColumn("dbo.Jobs", "EndDate");
            DropColumn("dbo.Jobs", "CompletionDate");
            DropColumn("dbo.Jobs", "StartDate");
            DropColumn("dbo.Jobs", "JobCompletionStatusId");
            DropTable("dbo.JobCompletionStatus");
            CreateIndex("dbo.Orders", "Name", unique: true, name: "IX_OrderName");
        }
    }
}
