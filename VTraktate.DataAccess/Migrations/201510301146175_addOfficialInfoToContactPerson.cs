namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOfficialInfoToContactPerson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonOfficialInfoes",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        TitleName = c.String(),
                        DepartmentName = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.PersonId);
            
            AddColumn("dbo.Orders", "ContactPerson_IsDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonOfficialInfoes", "PersonId", "dbo.People");
            DropIndex("dbo.PersonOfficialInfoes", new[] { "PersonId" });
            DropColumn("dbo.Orders", "ContactPerson_IsDefault");
            DropTable("dbo.PersonOfficialInfoes");
        }
    }
}
