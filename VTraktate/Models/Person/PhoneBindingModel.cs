using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Person
{
    public class PhoneBindingModel
    {
        public string Phone { get; set; }
        public string Ext { get; set; }
        public bool Active { get; set; }
        public int TypeId { get; set; }
        public string Comment { get; set; }
    }
}