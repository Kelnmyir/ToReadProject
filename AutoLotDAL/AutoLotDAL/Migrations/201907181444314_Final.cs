namespace AutoLotDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Customer_CustID", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CarId", "dbo.Inventory");
            DropIndex("dbo.Orders", new[] { "Customer_CustID" });
            DropColumn("dbo.Orders", "CustomerId");
            RenameColumn(table: "dbo.Orders", name: "Customer_CustID", newName: "CustomerId");
            DropPrimaryKey("dbo.Inventory");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.CreditRisks");
            DropColumn("dbo.Inventory", "CarID");
            DropColumn("dbo.Orders", "OrderID");
            DropColumn("dbo.Customers", "CustID");
            DropColumn("dbo.CreditRisks", "CustID");
            AddColumn("dbo.Inventory", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Inventory", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Orders", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customers", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.CreditRisks", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CreditRisks", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AlterColumn("dbo.Orders", "CustomerId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Inventory", "Id");
            AddPrimaryKey("dbo.Orders", "Id");
            AddPrimaryKey("dbo.Customers", "Id");
            AddPrimaryKey("dbo.CreditRisks", "Id");
            CreateIndex("dbo.Orders", "CustomerId");
            CreateIndex("dbo.CreditRisks", new[] { "LastName", "FirstName" }, unique: true, name: "IDX_CreditRisk_Name");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "CarId", "dbo.Inventory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CarId", "dbo.Inventory");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropPrimaryKey("dbo.CreditRisks");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Inventory");
            DropColumn("dbo.CreditRisks", "Timestamp");
            DropColumn("dbo.CreditRisks", "Id");
            DropColumn("dbo.Customers", "Timestamp");
            DropColumn("dbo.Customers", "Id");
            DropColumn("dbo.Orders", "Timestamp");
            DropColumn("dbo.Orders", "Id");
            DropColumn("dbo.Inventory", "Timestamp");
            DropColumn("dbo.Inventory", "Id");
            DropIndex("dbo.CreditRisks", "IDX_CreditRisk_Name");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            AddColumn("dbo.CreditRisks", "CustID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customers", "CustID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Orders", "OrderID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Inventory", "CarID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Orders", "CustomerId", c => c.Int());
            AddPrimaryKey("dbo.CreditRisks", "CustID");
            AddPrimaryKey("dbo.Customers", "CustID");
            AddPrimaryKey("dbo.Orders", "OrderID");
            AddPrimaryKey("dbo.Inventory", "CarID");
            RenameColumn(table: "dbo.Orders", name: "CustomerId", newName: "Customer_CustID");
            AddColumn("dbo.Orders", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Customer_CustID");
            AddForeignKey("dbo.Orders", "CarId", "dbo.Inventory", "CarID");
            AddForeignKey("dbo.Orders", "Customer_CustID", "dbo.Customers", "CustID");
        }
    }
}
