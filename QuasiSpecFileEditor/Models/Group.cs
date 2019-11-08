using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Models
{
    public class Group : EntityBase
    {
        public int GroupID { get; set; }

        //[Required]
        public string GroupName { get; set; }

        public int ModelID { get; set; }

        public virtual Model Model { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
    }
}