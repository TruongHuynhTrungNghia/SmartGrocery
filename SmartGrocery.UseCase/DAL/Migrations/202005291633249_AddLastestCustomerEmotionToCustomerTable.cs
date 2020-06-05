namespace SmartGrocery.UseCase.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddLastestCustomerEmotionToCustomerTable : DbMigration
    {
        public override void Down()
        {
            DropColumn("customer", "EmotionProbability");
            DropColumn("customer", "LastestCustomerEmotion");
        }

        public override void Up()
        {
            AddColumn("customer", "LastestCustomerEmotion", c => c.String(maxLength: 32, storeType: "nvarchar"));
            AddColumn("customer", "EmotionProbability", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}