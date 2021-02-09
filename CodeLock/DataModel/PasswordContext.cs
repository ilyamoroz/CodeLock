namespace CodeLock.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PasswordContext : DbContext
    {
        
        public PasswordContext()
            : base("name=PasswordContext")
        {
        }
        public DbSet<Password> passwords { get; set; }
        public DbSet<Admin> admins { get; set; }
    }

   
}