namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class services_languages_Domains : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProviderId = c.Int(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                        QA_Grade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QA_Stars = c.Int(nullable: false),
                        QA_Comment = c.String(),
                        Rate_Minrate = c.Decimal(precision: 18, scale: 2),
                        Rate_MaxRate = c.Decimal(precision: 18, scale: 2),
                        CurrencyId = c.Int(nullable: false),
                        ServiceUOMId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceUOMs", t => t.ServiceUOMId, cascadeDelete: true)
                .Index(t => t.ProviderId)
                .Index(t => t.ServiceTypeId)
                .Index(t => t.CurrencyId)
                .Index(t => t.ServiceUOMId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceLanguageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        LanguagePairId = c.Int(nullable: false),
                        QA_Grade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QA_Stars = c.Int(nullable: false),
                        QA_Comment = c.String(),
                        Rate_Minrate = c.Decimal(precision: 18, scale: 2),
                        Rate_MaxRate = c.Decimal(precision: 18, scale: 2),
                        Comment = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.LanguagePairs", t => t.LanguagePairId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.LanguagePairId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.ServiceDomainInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DomainId = c.Int(nullable: false),
                        QA_Grade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QA_Stars = c.Int(nullable: false),
                        QA_Comment = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                        ServiceLanguageInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.TranslationDomains", t => t.DomainId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.ServiceLanguageInfoes", t => t.ServiceLanguageInfo_Id)
                .Index(t => t.DomainId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.ServiceLanguageInfo_Id);
            
            CreateTable(
                "dbo.TranslationDomains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ParentId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatedById)
                .ForeignKey("dbo.People", t => t.ModifiedById)
                .ForeignKey("dbo.TranslationDomains", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.LanguagePairs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SpecifyLanguage = c.Boolean(nullable: false),
                        SpecifyDomains = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceUOMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "ServiceUOMId", "dbo.ServiceUOMs");
            DropForeignKey("dbo.Services", "ServiceTypeId", "dbo.ServiceTypes");
            DropForeignKey("dbo.Services", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Services", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.ServiceLanguageInfoes", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ServiceLanguageInfoes", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.ServiceLanguageInfoes", "LanguagePairId", "dbo.LanguagePairs");
            DropForeignKey("dbo.ServiceDomainInfoes", "ServiceLanguageInfo_Id", "dbo.ServiceLanguageInfoes");
            DropForeignKey("dbo.ServiceDomainInfoes", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.ServiceDomainInfoes", "DomainId", "dbo.TranslationDomains");
            DropForeignKey("dbo.TranslationDomains", "ParentId", "dbo.TranslationDomains");
            DropForeignKey("dbo.TranslationDomains", "ModifiedById", "dbo.People");
            DropForeignKey("dbo.TranslationDomains", "CreatedById", "dbo.People");
            DropForeignKey("dbo.ServiceDomainInfoes", "CreatedById", "dbo.People");
            DropForeignKey("dbo.ServiceLanguageInfoes", "CreatedById", "dbo.People");
            DropForeignKey("dbo.Services", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Services", "CreatedById", "dbo.People");
            DropIndex("dbo.TranslationDomains", new[] { "ModifiedById" });
            DropIndex("dbo.TranslationDomains", new[] { "CreatedById" });
            DropIndex("dbo.TranslationDomains", new[] { "ParentId" });
            DropIndex("dbo.ServiceDomainInfoes", new[] { "ServiceLanguageInfo_Id" });
            DropIndex("dbo.ServiceDomainInfoes", new[] { "ModifiedById" });
            DropIndex("dbo.ServiceDomainInfoes", new[] { "CreatedById" });
            DropIndex("dbo.ServiceDomainInfoes", new[] { "DomainId" });
            DropIndex("dbo.ServiceLanguageInfoes", new[] { "ModifiedById" });
            DropIndex("dbo.ServiceLanguageInfoes", new[] { "CreatedById" });
            DropIndex("dbo.ServiceLanguageInfoes", new[] { "LanguagePairId" });
            DropIndex("dbo.ServiceLanguageInfoes", new[] { "ServiceId" });
            DropIndex("dbo.Services", new[] { "ModifiedById" });
            DropIndex("dbo.Services", new[] { "CreatedById" });
            DropIndex("dbo.Services", new[] { "ServiceUOMId" });
            DropIndex("dbo.Services", new[] { "CurrencyId" });
            DropIndex("dbo.Services", new[] { "ServiceTypeId" });
            DropIndex("dbo.Services", new[] { "ProviderId" });
            DropTable("dbo.ServiceUOMs");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.LanguagePairs");
            DropTable("dbo.TranslationDomains");
            DropTable("dbo.ServiceDomainInfoes");
            DropTable("dbo.ServiceLanguageInfoes");
            DropTable("dbo.Currencies");
            DropTable("dbo.Services");
        }
    }
}
