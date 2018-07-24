namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblDiscount_editColType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Discount", "ValueType", c => c.Byte());
            DropColumn("dbo.CMS_Discount", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CMS_Discount", "Type", c => c.Byte());
            DropColumn("dbo.CMS_Discount", "ValueType");
        }
    }
}
