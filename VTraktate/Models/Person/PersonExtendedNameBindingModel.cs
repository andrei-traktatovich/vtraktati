using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Person
{
    public class PersonExtendedNameBindingModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Initials { get; set; }
        public string AlternateName { get; set; }
    }
}