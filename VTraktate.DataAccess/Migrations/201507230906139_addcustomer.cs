namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Protected = c.Boolean(nullable: false),
                        Hidden = c.Boolean(nullable: false),
                        IsIndividual = c.Boolean(nullable: false),
                        NumberPerOffice = c.Boolean(nullable: false),
                        LongName = c.String(),
                        ShortName = c.String(),
                        Code = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.Customers", "CreatedById", "dbo.People");
            DropIndex("dbo.Customers", new[] { "ModifiedById" });
            DropIndex("dbo.Customers", new[] { "CreatedById" });
            DropTable("dbo.Customers");
        }
    }
}
