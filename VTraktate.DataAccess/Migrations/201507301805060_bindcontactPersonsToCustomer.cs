namespace VTraktate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bindcontactPersonsToCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonCustomers",
                c => new
                    {
                        Person_Id = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.Customer_Id })
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonCustomers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.PersonCustomers", "Person_Id", "dbo.People");
            DropIndex("dbo.PersonCustomers", new[] { "Customer_Id" });
            DropIndex("dbo.PersonCustomers", new[] { "Person_Id" });
            DropTable("dbo.PersonCustomers");
        }
    }
}
