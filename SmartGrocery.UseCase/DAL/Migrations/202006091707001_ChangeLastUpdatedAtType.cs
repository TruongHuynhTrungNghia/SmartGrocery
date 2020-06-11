namespace SmartGrocery.UseCase.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLastUpdatedAtType : DbMigration
    {
        public override void Down()
        {
            AlterColumn("transaction", "LastUpdatedAt", c => c.String(nullable: false, unicode: false));
            AlterColumn("transaction", "LastUpdatedBy", c => c.String(nullable: false, unicode: false));
            AlterColumn("transaction", "CreatedBy", c => c.String(nullable: false, unicode: false));
        }

        public override void Up()
        {
            AlterColumn("transaction", "CreatedBy", c => c.String(unicode: false));
            AlterColumn("transaction", "LastUpdatedBy", c => c.String(unicode: false));
            Sql(@"ALTER TABLE `transaction` CHANGE COLUMN `LastUpdatedAt` `LastUpdatedAt` LONGTEXT NULL;
                  UPDATE `transaction` SET  `LastUpdatedAt` = NULL;
                  ALTER TABLE `transaction` 
                    CHANGE COLUMN `LastUpdatedAt` `LastUpdatedAt` DATETIME NULL DEFAULT NULL");
        }
    }
}