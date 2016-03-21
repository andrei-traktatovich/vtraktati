using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class EmailBindingModel
    {
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }
    }
}