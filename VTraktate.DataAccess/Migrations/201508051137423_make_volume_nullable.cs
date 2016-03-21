namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class make_volume_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "Initial_Volume_Pages", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Jobs", "Initial_Volume_Chars", c => c.Int());
            AlterColumn("dbo.Jobs", "Initial_Volume_Words", c => c.Int());
            AlterColumn("dbo.Jobs", "Final_Volume_Pages", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Jobs", "Final_Volume_Chars", c => c.Int());
            AlterColumn("dbo.Jobs", "Final_Volume_Words", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "Final_Volume_Words", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "Final_Volume_Chars", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "Final_Volume_Pages", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Jobs", "Initial_Volume_Words", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "Initial_Volume_Chars", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "Initial_Volume_Pages", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
