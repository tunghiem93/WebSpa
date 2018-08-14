namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblProduct_editColForProcedure : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CMS_Products", "ShortDescription", c => c.String(maxLength: 2000));
            AlterColumn("dbo.CMS_Products", "Process", c => c.String(maxLength: 2000));
            AlterColumn("dbo.CMS_Products", "SpaTreatment", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CMS_Products", "SpaTreatment", c => c.String(maxLength: 500));
            AlterColumn("dbo.CMS_Products", "Process", c => c.String(maxLength: 500));
            AlterColumn("dbo.CMS_Products", "ShortDescription", c => c.String(maxLength: 500));
        }
    }
}
