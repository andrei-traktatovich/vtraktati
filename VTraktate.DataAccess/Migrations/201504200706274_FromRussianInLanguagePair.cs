namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FromRussianInLanguagePair : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LanguagePairs", "FromRussian", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LanguagePairs", "FromRussian");
        }
    }
}
