namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class personname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "PersonName_FirstName", c => c.String());
            AddColumn("dbo.People", "PersonName_MiddleName", c => c.String());
            AddColumn("dbo.People", "PersonName_LastName", c => c.String());
            AddColumn("dbo.People", "PersonName_FullName", c => c.String());
            AddColumn("dbo.People", "PersonName_AlternateName", c => c.String());
            AddColumn("dbo.People", "PersonName_AddressName", c => c.String());
            AddColumn("dbo.People", "PersonName_Initials", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "PersonName_Initials");
            DropColumn("dbo.People", "PersonName_AddressName");
            DropColumn("dbo.People", "PersonName_AlternateName");
            DropColumn("dbo.People", "PersonName_FullName");
            DropColumn("dbo.People", "PersonName_LastName");
            DropColumn("dbo.People", "PersonName_MiddleName");
            DropColumn("dbo.People", "PersonName_FirstName");
        }
    }
}
