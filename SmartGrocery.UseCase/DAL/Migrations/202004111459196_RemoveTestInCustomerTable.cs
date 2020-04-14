namespace SmartGrocery.UseCase.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveTestInCustomerTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("customer", "Test");
        }

        public override void Down()
        {
            AddColumn("customer", "Test", c => c.String(unicode: false));
        }
    }
}