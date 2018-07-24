namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblCustomer_editColName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Customer", "FirstName", c => c.String(maxLength: 50));
            AddColumn("dbo.CMS_Customer", "LastName", c => c.String(maxLength: 50));
            DropColumn("dbo.CMS_Customer", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CMS_Customer", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.CMS_Customer", "LastName");
            DropColumn("dbo.CMS_Customer", "FirstName");
        }
    }
}
