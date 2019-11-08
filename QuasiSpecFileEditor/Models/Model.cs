using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Models
{
    public class Model
    {
        public int ModelID { get; set; }

        public string CodeName { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Part> Parts { get; set; }

        public virtual ICollection<AlarmDescription> AlarmDescriptions { get; set; }
    }
}