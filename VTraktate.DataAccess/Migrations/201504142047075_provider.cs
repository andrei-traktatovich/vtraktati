namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class provider : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProviderTypeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Int(),
                        ModifiedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedBy_Id)
                .ForeignKey("dbo.People", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.ProviderTypes", t => t.ProviderTypeId, cascadeDelete: true)
                .Index(t => t.ProviderTypeId)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.ProviderTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Providers", "ProviderTypeId", "dbo.ProviderTypes");
            DropForeignKey("dbo.Providers", "ModifiedBy_Id", "dbo.People");
            DropForeignKey("dbo.Providers", "CreatedBy_Id", "dbo.People");
            DropIndex("dbo.Providers", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Providers", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Providers", new[] { "ProviderTypeId" });
            DropTable("dbo.ProviderTypes");
            DropTable("dbo.Providers");
        }
    }
}
