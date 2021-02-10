namespace CodeLock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attempts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginAttempts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Passwords = c.String(nullable: false, maxLength: 1000),
                        MacAddress = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginAttempts");
        }
    }
}
