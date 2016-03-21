namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_phones_other_contacts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OtherContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        TypeId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Comment = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.OtherContactTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.TypeId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.OtherContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Ext = c.String(),
                        Active = c.Boolean(nullable: false),
                        TypeId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Comment = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.PhoneTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.TypeId)
                .Index(t => t.PersonId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.Person_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Phones", "TypeId", "dbo.PhoneTypes");
            DropForeignKey("dbo.Phones", "PersonId", "dbo.People");
            DropForeignKey("dbo.Phones", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.Phones", "CreatedById", "dbo.People");
            DropForeignKey("dbo.OtherContacts", "Person_Id", "dbo.People");
            DropForeignKey("dbo.OtherContacts", "TypeId", "dbo.OtherContactTypes");
            DropForeignKey("dbo.OtherContacts", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.OtherContacts", "CreatedById", "dbo.People");
            DropIndex("dbo.Phones", new[] { "Person_Id" });
            DropIndex("dbo.Phones", new[] { "ModifiedById" });
            DropIndex("dbo.Phones", new[] { "CreatedById" });
            DropIndex("dbo.Phones", new[] { "PersonId" });
            DropIndex("dbo.Phones", new[] { "TypeId" });
            DropIndex("dbo.OtherContacts", new[] { "Person_Id" });
            DropIndex("dbo.OtherContacts", new[] { "ModifiedById" });
            DropIndex("dbo.OtherContacts", new[] { "CreatedById" });
            DropIndex("dbo.OtherContacts", new[] { "TypeId" });
            DropTable("dbo.Phones");
            DropTable("dbo.OtherContactTypes");
            DropTable("dbo.OtherContacts");
        }
    }
}
