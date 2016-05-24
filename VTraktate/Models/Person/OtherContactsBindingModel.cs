using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Person
{
    public class OtherContactsBindingModel
    {
        public string Address { get; set; }
        public int Type { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }
    }
}