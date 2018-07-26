namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTblCustomer_AddColCustomerType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Customer", "CustomerType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Customer", "CustomerType");
        }
    }
}
