namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblEmployee_addColRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Employee", "RoleID", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Employee", "RoleID");
        }
    }
}
