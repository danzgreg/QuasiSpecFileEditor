using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Models
{
    public abstract class EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastModifyDate { get; set; }

        public string LastModifiedBy { get; set; }
    }
}