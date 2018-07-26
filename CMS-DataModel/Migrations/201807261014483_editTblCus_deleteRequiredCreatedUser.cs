namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblCus_deleteRequiredCreatedUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CMS_Customer", "CreatedUser", c => c.String(maxLength: 100));
            AlterColumn("dbo.CMS_Customer", "ModifiedUser", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CMS_Customer", "ModifiedUser", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CMS_Customer", "CreatedUser", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
