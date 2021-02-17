namespace CodeLock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adminspass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passwords", "AdminsPass", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passwords", "AdminsPass");
        }
    }
}
