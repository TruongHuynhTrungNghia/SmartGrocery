namespace SmartGrocery.UseCase.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImplementedProductSnapshot : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("base_product", "Transaction_Id", "transaction");
            DropIndex("base_product", new[] { "Transaction_Id" });
            CreateTable(
                "dbo.ProductSnapshots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        TransactionId = c.Guid(nullable: false),
                        NumberOfSoldProduct = c.Int(nullable: false),
                        RemainingProduct = c.Int(nullable: false),
                        Status = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("base_product", t => t.ProductId)
                .ForeignKey("transaction", t => t.TransactionId)
                .Index(t => t.ProductId)
                .Index(t => t.TransactionId);
            
            DropColumn("base_product", "Transaction_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.base_product", "Transaction_Id", c => c.Guid());
            DropForeignKey("dbo.ProductSnapshots", "TransactionId", "dbo.transaction");
            DropForeignKey("dbo.ProductSnapshots", "ProductId", "dbo.base_product");
            DropIndex("dbo.ProductSnapshots", new[] { "TransactionId" });
            DropIndex("dbo.ProductSnapshots", new[] { "ProductId" });
            DropTable("dbo.ProductSnapshots");
            CreateIndex("dbo.base_product", "Transaction_Id");
            AddForeignKey("dbo.base_product", "Transaction_Id", "dbo.transaction", "Id");
        }
    }
}
