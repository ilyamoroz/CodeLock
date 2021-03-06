using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLock.DataModel
{
    public class Password
    {
        public int PasswordID { get; set; }  

        [MaxLength(1000)]
        [Required]
        public string Pass { get; set; }
        [Required]
        public string Available { get; set; }

        [Required]
        public string Deleted { get; set; }

        [Required]
        public int AdminID { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
