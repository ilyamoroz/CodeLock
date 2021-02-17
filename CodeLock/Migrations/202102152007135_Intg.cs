namespace CodeLock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Passwords", "AdminsPass", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Passwords", "AdminsPass", c => c.String(nullable: false));
        }
    }
}
