namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_native_speaker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceLanguageInfoes", "NativeSpeaker", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceLanguageInfoes", "NativeSpeaker");
        }
    }
}
