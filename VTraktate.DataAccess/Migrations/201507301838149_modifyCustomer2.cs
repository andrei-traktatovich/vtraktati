namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyCustomer2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "RoundingPolicyId", "dbo.RoundingPolicies");
            DropIndex("dbo.Customers", new[] { "RoundingPolicyId" });
            AlterColumn("dbo.Customers", "RoundingPolicyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "RoundingPolicyId");
            AddForeignKey("dbo.Customers", "RoundingPolicyId", "dbo.RoundingPolicies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "RoundingPolicyId", "dbo.RoundingPolicies");
            DropIndex("dbo.Customers", new[] { "RoundingPolicyId" });
            AlterColumn("dbo.Customers", "RoundingPolicyId", c => c.Int());
            CreateIndex("dbo.Customers", "RoundingPolicyId");
            AddForeignKey("dbo.Customers", "RoundingPolicyId", "dbo.RoundingPolicies", "Id");
        }
    }
}
