namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyCustomer1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RoundingPolicies", "IX_CustomerCode");
            AlterColumn("dbo.RoundingPolicies", "Name", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.RoundingPolicies", "Name", unique: true, name: "IX_RoundingPolicyName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RoundingPolicies", "IX_RoundingPolicyName");
            AlterColumn("dbo.RoundingPolicies", "Name", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.RoundingPolicies", "Name", unique: true, name: "IX_CustomerCode");
        }
    }
}
