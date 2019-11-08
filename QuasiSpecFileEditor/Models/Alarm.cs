using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Models
{
    public class Alarm
    {
        public int AlarmID { get; set; }

        public string AlarmName { get; set; }

        public string Comment { get; set; }

        public virtual ICollection<AlarmDescription> AlarmDescriptions { get; set; }
    }
}