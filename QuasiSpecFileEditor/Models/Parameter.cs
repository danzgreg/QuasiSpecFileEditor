using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Models
{
    public class Parameter : EntityBase
    {
        public int ParameterID { get; set; }

        public string FieldName { get; set; }

        public double? UpperLimit { get; set; }

        public string UpperLimitDefect { get; set; }

        public double? LowerLimit { get; set; }

        public string LowerLimitDefect { get; set; }

        public string NegFactor { get; set; }

        public string NegFactorDefect { get; set; }

        public string OpReqUpload { get; set; }

        public string ParamTest { get; set; }

        public string CSVHeaderName { get; set; }

        public virtual ICollection<Group> Groups { get; set; }


    }
}