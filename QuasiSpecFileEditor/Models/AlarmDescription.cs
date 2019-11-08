using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Models
{
    public class AlarmDescription : EntityBase
    {
        public int AlarmDescriptionID { get; set; }

        public int ModelID { get; set; }

        public int AlarmID { get; set; }

        public int? Priority { get; set; }

        public int? Criteria { get; set; }

        public string Hold { get; set; }

        public string ParameterName { get; set; }

        public string TargetLimit { get; set; }

        public virtual Model Model { get; set; }

        public virtual Alarm Alarm { get; set; }
    }
}