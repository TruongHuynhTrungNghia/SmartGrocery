namespace SmartGrocery.UseCase.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTransactionAndProductSnapshotRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ProductSnapshots", "TransactionId", "transaction");
            AddForeignKey("ProductSnapshots", "TransactionId", "transaction", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("ProductSnapshots", "TransactionId", "transaction");
            AddForeignKey("ProductSnapshots", "TransactionId", "transaction", "Id");
        }
    }
}