using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Models
{
    [NotMapped]
    public class User
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string SAMAccountName { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public string Department { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Role { get; set; }

        //public bool isMapped { get; set; }
    }
}