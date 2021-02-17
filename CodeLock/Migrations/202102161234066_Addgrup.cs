namespace CodeLock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addgrup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passwords", "Admin_AdminID", c => c.Int());
            CreateIndex("dbo.Passwords", "Admin_AdminID");
            AddForeignKey("dbo.Passwords", "Admin_AdminID", "dbo.Admins", "AdminID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passwords", "Admin_AdminID", "dbo.Admins");
            DropIndex("dbo.Passwords", new[] { "Admin_AdminID" });
            DropColumn("dbo.Passwords", "Admin_AdminID");
        }
    }
}
