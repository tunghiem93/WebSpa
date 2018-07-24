namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblProduct_editColTypeCode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CMS_Products", "TypeCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CMS_Products", "TypeCode", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
