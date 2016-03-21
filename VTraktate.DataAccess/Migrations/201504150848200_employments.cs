namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Providers", "Office_Id", "dbo.Offices");
            RenameColumn(table: "dbo.People", name: "CreatedBy_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.People", name: "ModifiedBy_Id", newName: "ModifiedById");
            RenameColumn(table: "dbo.Providers", name: "CreatedBy_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.Providers", name: "ModifiedBy_Id", newName: "ModifiedById");
            RenameIndex(table: "dbo.People", name: "IX_CreatedBy_Id", newName: "IX_CreatedById");
            RenameIndex(table: "dbo.People", name: "IX_ModifiedBy_Id", newName: "IX_ModifiedById");
            RenameIndex(table: "dbo.Providers", name: "IX_CreatedBy_Id", newName: "IX_CreatedById");
            RenameIndex(table: "dbo.Providers", name: "IX_ModifiedBy_Id", newName: "IX_ModifiedById");
            CreateTable(
                "dbo.Employments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfficeID = c.Int(nullable: false),
                        TitleId = c.Int(nullable: false),
                        Comment = c.String(),
                        StatusID = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        ProviderId = c.Int(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.Offices", t => t.OfficeID, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.EmploymentStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.Titles", t => t.TitleId, cascadeDelete: true)
                .Index(t => t.OfficeID)
                .Index(t => t.TitleId)
                .Index(t => t.StatusID)
                .Index(t => t.ProviderId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.EmploymentStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedById = c.Int(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            AddColumn("dbo.Offices", "OfficialName", c => c.String());
            AddColumn("dbo.Offices", "Code", c => c.String(maxLength: 10));
            AddForeignKey("dbo.Providers", "Office_Id", "dbo.Offices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Providers", "Office_Id", "dbo.Offices");
            DropForeignKey("dbo.Employments", "TitleId", "dbo.Titles");
            DropForeignKey("dbo.Titles", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.Titles", "CreatedById", "dbo.People");
            DropForeignKey("dbo.Employments", "StatusID", "dbo.EmploymentStatus");
            DropForeignKey("dbo.Employments", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Employments", "OfficeID", "dbo.Offices");
            DropForeignKey("dbo.Employments", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.Employments", "CreatedById", "dbo.People");
            DropIndex("dbo.Titles", new[] { "ModifiedById" });
            DropIndex("dbo.Titles", new[] { "CreatedById" });
            DropIndex("dbo.Employments", new[] { "ModifiedById" });
            DropIndex("dbo.Employments", new[] { "CreatedById" });
            DropIndex("dbo.Employments", new[] { "ProviderId" });
            DropIndex("dbo.Employments", new[] { "StatusID" });
            DropIndex("dbo.Employments", new[] { "TitleId" });
            DropIndex("dbo.Employments", new[] { "OfficeID" });
            DropColumn("dbo.Offices", "Code");
            DropColumn("dbo.Offices", "OfficialName");
            DropTable("dbo.Titles");
            DropTable("dbo.EmploymentStatus");
            DropTable("dbo.Employments");
            RenameIndex(table: "dbo.Providers", name: "IX_ModifiedById", newName: "IX_ModifiedBy_Id");
            RenameIndex(table: "dbo.Providers", name: "IX_CreatedById", newName: "IX_CreatedBy_Id");
            RenameIndex(table: "dbo.People", name: "IX_ModifiedById", newName: "IX_ModifiedBy_Id");
            RenameIndex(table: "dbo.People", name: "IX_CreatedById", newName: "IX_CreatedBy_Id");
            RenameColumn(table: "dbo.Providers", name: "ModifiedById", newName: "ModifiedBy_Id");
            RenameColumn(table: "dbo.Providers", name: "CreatedById", newName: "CreatedBy_Id");
            RenameColumn(table: "dbo.People", name: "ModifiedById", newName: "ModifiedBy_Id");
            RenameColumn(table: "dbo.People", name: "CreatedById", newName: "CreatedBy_Id");
            AddForeignKey("dbo.Providers", "Office_Id", "dbo.Offices", "Id", cascadeDelete: true);
        }
    }
}
