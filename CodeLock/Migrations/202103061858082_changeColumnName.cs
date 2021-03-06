namespace CodeLock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeColumnName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Passwords", name: "AdminID", newName: "AdminPasswordID");
            RenameIndex(table: "dbo.Passwords", name: "IX_AdminID", newName: "IX_AdminPasswordID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Passwords", name: "IX_AdminPasswordID", newName: "IX_AdminID");
            RenameColumn(table: "dbo.Passwords", name: "AdminPasswordID", newName: "AdminID");
        }
    }
}
