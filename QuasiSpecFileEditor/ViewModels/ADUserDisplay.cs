using QuasiSpecFileEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.ViewModels
{
    public class ADUserDisplay
    {
        public List<User> ADUsers { get; set; }

        public string CurrentUserDisplayName { get; set; }
    }
}