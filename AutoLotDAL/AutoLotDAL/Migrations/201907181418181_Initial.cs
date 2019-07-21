namespace AutoLotDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventory",
                c => new
                    {
                        CarID = c.Int(nullable: false, identity: true),
                        Make = c.String(maxLength: 50),
                        Color = c.String(maxLength: 50),
                        PetName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CarID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        Customer_CustID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.Customer_CustID)
                .ForeignKey("dbo.Inventory", t => t.CarId)
                .Index(t => t.CarId)
                .Index(t => t.Customer_CustID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CustID);
            
            CreateTable(
                "dbo.CreditRisks",
                c => new
                    {
                        CustID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CustID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CarId", "dbo.Inventory");
            DropForeignKey("dbo.Orders", "Customer_CustID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customer_CustID" });
            DropIndex("dbo.Orders", new[] { "CarId" });
            DropTable("dbo.CreditRisks");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.Inventory");
        }
    }
}
