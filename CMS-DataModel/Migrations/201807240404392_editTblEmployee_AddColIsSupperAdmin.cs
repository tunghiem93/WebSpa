namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblEmployee_AddColIsSupperAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Employee", "IsSupperAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Employee", "IsSupperAdmin");
        }
    }
}
