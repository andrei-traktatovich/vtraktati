namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_grade_extra_foreignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Grades", "ServiceDomainInfo_Id", "dbo.ServiceDomainInfoes");
            DropIndex("dbo.Grades", new[] { "ServiceDomainInfo_Id" });
            DropColumn("dbo.Grades", "ServiceDomainInfo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Grades", "ServiceDomainInfo_Id", c => c.Int());
            CreateIndex("dbo.Grades", "ServiceDomainInfo_Id");
            AddForeignKey("dbo.Grades", "ServiceDomainInfo_Id", "dbo.ServiceDomainInfoes", "Id");
        }
    }
}
