namespace SmartGrocery.UseCase.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCustomerIdNumberAndEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.customer", "Email", c => c.String(unicode: false));
            AddColumn("dbo.customer", "IdNumber", c => c.String(unicode: false));
        }

        public override void Down()
        {
            DropColumn("dbo.customer", "IdNumber");
            DropColumn("dbo.customer", "Email");
        }
    }
}