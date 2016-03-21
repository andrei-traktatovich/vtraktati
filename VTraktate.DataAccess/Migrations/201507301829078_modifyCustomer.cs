namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoundingPolicies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_CustomerCode");
            
            AddColumn("dbo.Customers", "RoundingPolicyId", c => c.Int());
            CreateIndex("dbo.Customers", "RoundingPolicyId");
            AddForeignKey("dbo.Customers", "RoundingPolicyId", "dbo.RoundingPolicies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "RoundingPolicyId", "dbo.RoundingPolicies");
            DropIndex("dbo.RoundingPolicies", "IX_CustomerCode");
            DropIndex("dbo.Customers", new[] { "RoundingPolicyId" });
            DropColumn("dbo.Customers", "RoundingPolicyId");
            DropTable("dbo.RoundingPolicies");
        }
    }
}
