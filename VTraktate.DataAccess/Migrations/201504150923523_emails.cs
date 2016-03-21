namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address_Email = c.String(),
                        Address_Comment = c.String(),
                        Address_Active = c.Boolean(nullable: false),
                        PersonId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "PersonId", "dbo.People");
            DropForeignKey("dbo.Emails", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.Emails", "CreatedById", "dbo.People");
            DropIndex("dbo.Emails", new[] { "ModifiedById" });
            DropIndex("dbo.Emails", new[] { "CreatedById" });
            DropIndex("dbo.Emails", new[] { "PersonId" });
            DropTable("dbo.Emails");
        }
    }
}
