namespace CodeLock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passwords", "Deleted", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passwords", "Deleted");
        }
    }
}
