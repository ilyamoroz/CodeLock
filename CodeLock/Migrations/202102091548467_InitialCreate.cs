namespace CodeLock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        PasswordID = c.Int(nullable: false, identity: true),
                        Pass = c.String(nullable: false, maxLength: 1000),
                        Available = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PasswordID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Passwords");
        }
    }
}
