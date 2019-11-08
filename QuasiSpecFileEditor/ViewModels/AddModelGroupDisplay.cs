using QuasiSpecFileEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.ViewModels
{
    public class AddModelGroupDisplay
    {
        public Group Group { get; set; }

        public List<Part> Parts { get; set; }

        public List<Parameter> Parameters { get; set; }

        public int[] SelectedParts { get; set; }

        public int[] SelectedParameters { get; set; }
    }
}