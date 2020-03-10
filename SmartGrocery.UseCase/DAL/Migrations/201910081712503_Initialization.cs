namespace SmartGrocery.UseCase.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.base_product",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false, precision: 0),
                        ManufacturingDate = c.DateTime(nullable: false, precision: 0),
                        ProductNumber = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Transaction_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.transaction", t => t.Transaction_Id)
                .Index(t => t.Transaction_Id);
            
            CreateTable(
                "dbo.customer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(nullable: false, unicode: false),
                        FirstName = c.String(nullable: false, unicode: false),
                        CustomerId = c.String(nullable: false, unicode: false),
                        DateOfBirth = c.DateTime(nullable: false, precision: 0),
                        Age = c.Int(nullable: false),
                        Points = c.Int(),
                        Test = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.transaction",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TransactionNumber = c.String(unicode: false),
                        Amount = c.String(nullable: false, unicode: false),
                        CreatedBy = c.String(nullable: false, unicode: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        LastUpdatedBy = c.String(nullable: false, unicode: false),
                        LastUpdatedAt = c.String(nullable: false, unicode: false),
                        CustomerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.permission",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Description = c.String(maxLength: 350, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.role",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.permissions-in-roles",
                c => new
                    {
                        Permission_Id = c.Guid(nullable: false),
                        Role_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.Role_Id })
                .ForeignKey("dbo.permission", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.role", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.permissions-in-roles", "Role_Id", "dbo.role");
            DropForeignKey("dbo.permissions-in-roles", "Permission_Id", "dbo.permission");
            DropForeignKey("dbo.transaction", "CustomerId", "dbo.customer");
            DropForeignKey("dbo.base_product", "Transaction_Id", "dbo.transaction");
            DropIndex("dbo.permissions-in-roles", new[] { "Role_Id" });
            DropIndex("dbo.permissions-in-roles", new[] { "Permission_Id" });
            DropIndex("dbo.transaction", new[] { "CustomerId" });
            DropIndex("dbo.base_product", new[] { "Transaction_Id" });
            DropTable("dbo.permissions-in-roles");
            DropTable("dbo.role");
            DropTable("dbo.permission");
            DropTable("dbo.transaction");
            DropTable("dbo.customer");
            DropTable("dbo.base_product");
        }
    }
}
