namespace EmpMgtSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edited : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccounts", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserAccounts", "StaffID", c => c.String(nullable: false));
            AlterColumn("dbo.UserAccounts", "ConfirmPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccounts", "ConfirmPassword", c => c.String());
            AlterColumn("dbo.UserAccounts", "StaffID", c => c.String());
            DropColumn("dbo.UserAccounts", "DateCreated");
        }
    }
}
