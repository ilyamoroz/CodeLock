using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLock.DataModel
{
    public class Admin
    {
        public int AdminID { get; set; }

        [MaxLength(1000)]
        [Required]
        public string AdminPassword { get; set; }

        public virtual ICollection<Password> PasswordsAdmin { get; set; }
    }
}
