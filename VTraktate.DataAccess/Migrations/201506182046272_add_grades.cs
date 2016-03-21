namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_grades : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        JobName = c.String(),
                        Comment = c.String(),
                        Error_Spelling = c.Boolean(nullable: false),
                        Error_Fact = c.Boolean(nullable: false),
                        Error_Term = c.Boolean(nullable: false),
                        Error_Sense = c.Boolean(nullable: false),
                        Error_Grammar = c.Boolean(nullable: false),
                        Error_Omissions = c.Boolean(nullable: false),
                        Error_Requirements = c.Boolean(nullable: false),
                        Bonus_NativeSpeaker = c.Boolean(nullable: false),
                        Bonus_Quality = c.Boolean(nullable: false),
                        JobPartId = c.Int(),
                        ProviderId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.JobParts", t => t.JobPartId)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .Index(t => t.JobPartId)
                .Index(t => t.ProviderId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Grades", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.Grades", "JobPartId", "dbo.JobParts");
            DropForeignKey("dbo.Grades", "CreatedById", "dbo.People");
            DropIndex("dbo.Grades", new[] { "ModifiedById" });
            DropIndex("dbo.Grades", new[] { "CreatedById" });
            DropIndex("dbo.Grades", new[] { "ProviderId" });
            DropIndex("dbo.Grades", new[] { "JobPartId" });
            DropTable("dbo.Grades");
        }
    }
}
