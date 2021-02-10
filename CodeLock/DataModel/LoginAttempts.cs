using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLock.DataModel
{
    public class LoginAttempts
    {
        public int ID { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Passwords { get; set; }

        [MaxLength(1000)]
        [Required]
        public string MacAddress { get; set; }

    }
}
