namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblDiscount_editColName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CMS_Discount", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CMS_Discount", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
