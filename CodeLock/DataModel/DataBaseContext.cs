namespace CodeLock.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataBaseContext : DbContext
    {
        
        public DataBaseContext()
            : base("name=PasswordContext")
        {
        }
        public DbSet<Password> passwords { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<LoginAttempts> attempts { get; set; }
    }

   
}