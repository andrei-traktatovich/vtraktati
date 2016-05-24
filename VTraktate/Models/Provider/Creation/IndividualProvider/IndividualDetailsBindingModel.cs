using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Provider.Creation.IndividualProvider
{
    public class IndividualDetailsBindingModel
    {
        public DateTime? BirthDay { get; set; }
        public int Regionid { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public int[] Groups { get; set; }
        public int LegalFormId { get; set; }
        public bool WorksNightly { get; set; }
        public int TimeDifference { get; set; }
        public string City { get; set; }
    }
}