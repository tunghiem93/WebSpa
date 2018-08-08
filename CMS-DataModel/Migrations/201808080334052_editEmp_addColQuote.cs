namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editEmp_addColQuote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Employee", "Quote", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Employee", "Quote");
        }
    }
}
