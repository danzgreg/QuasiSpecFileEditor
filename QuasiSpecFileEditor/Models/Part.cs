using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Models
{
    public class Part
    {
        public int PartID { get; set; }

        public string PN { get; set; }

        public int ModelID { get; set; }

        public int? GroupID { get; set; }

        public Model Model { get; set; }

        public Group Group { get; set; }
    }
}