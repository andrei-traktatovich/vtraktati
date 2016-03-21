namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyGradeModel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Grades", "ServiceTypeId", "dbo.ServiceTypes");
            DropIndex("dbo.Grades", new[] { "ServiceTypeId" });
            AlterColumn("dbo.Grades", "ServiceTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Grades", "ServiceTypeId");
            AddForeignKey("dbo.Grades", "ServiceTypeId", "dbo.ServiceTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "ServiceTypeId", "dbo.ServiceTypes");
            DropIndex("dbo.Grades", new[] { "ServiceTypeId" });
            AlterColumn("dbo.Grades", "ServiceTypeId", c => c.Int());
            CreateIndex("dbo.Grades", "ServiceTypeId");
            AddForeignKey("dbo.Grades", "ServiceTypeId", "dbo.ServiceTypes", "Id");
        }
    }
}
