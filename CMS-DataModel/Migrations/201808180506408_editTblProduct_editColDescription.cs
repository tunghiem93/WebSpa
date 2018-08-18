namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblProduct_editColDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CMS_Products", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CMS_Products", "Description", c => c.String(maxLength: 4000));
        }
    }
}
