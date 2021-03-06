namespace CodeLock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Passwords", "Admin_AdminID", "dbo.Admins");
            DropIndex("dbo.Passwords", new[] { "Admin_AdminID" });
            RenameColumn(table: "dbo.Passwords", name: "Admin_AdminID", newName: "AdminID");
            AlterColumn("dbo.Passwords", "AdminID", c => c.Int(nullable: false));
            CreateIndex("dbo.Passwords", "AdminID");
            AddForeignKey("dbo.Passwords", "AdminID", "dbo.Admins", "AdminID", cascadeDelete: true);
            DropColumn("dbo.Passwords", "AdminsPass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passwords", "AdminsPass", c => c.Int(nullable: false));
            DropForeignKey("dbo.Passwords", "AdminID", "dbo.Admins");
            DropIndex("dbo.Passwords", new[] { "AdminID" });
            AlterColumn("dbo.Passwords", "AdminID", c => c.Int());
            RenameColumn(table: "dbo.Passwords", name: "AdminID", newName: "Admin_AdminID");
            CreateIndex("dbo.Passwords", "Admin_AdminID");
            AddForeignKey("dbo.Passwords", "Admin_AdminID", "dbo.Admins", "AdminID");
        }
    }
}
