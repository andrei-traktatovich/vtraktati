namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ContactPerson_Comment = c.String(),
                        ContactPerson_FullName = c.String(),
                        ContactPerson_Phone = c.String(),
                        ContactPerson_Ext = c.String(),
                        ContactPerson_Email = c.String(),
                        CustomerId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "IX_OrderName")
                .Index(t => t.CustomerId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CreatedById", "dbo.People");
            DropIndex("dbo.Orders", new[] { "ModifiedById" });
            DropIndex("dbo.Orders", new[] { "CreatedById" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Orders", "IX_OrderName");
            DropTable("dbo.Orders");
        }
    }
}
