using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Models.Helpers;
using VTraktate.Models.Person;
using VTraktate.Models.Provider.Creation.Services;

namespace VTraktate.Models.Provider.Creation.IndividualProvider
{
    public class IndividualProviderModel
    {
        public IdNamePairBindingModel Type { get; set; }
        public PersonExtendedNameBindingModel PersonName { get; set; }
        public FreelanceBindingModel Freelance { get; set; }
        public EmploymentBindingModel Employment { get; set; }
        public EmailBindingModel[] Emails { get; set; }
        public PhoneBindingModel[] Telephones { get; set; }
        public OtherContactsBindingModel[] OtherContacts { get; set; }
        public ServiceBindingModel[] Services { get; set; }

        public DateTime? BirthDate { get; set; }
        public int Regionid { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public int[] Groups { get; set; }
        public int LegalFormId { get; set; }
        public bool WorksNightly { get; set; }
        public int TimeDifference { get; set; }
        public string City { get; set; }
        public int[] Soft { get; set; }
        public bool Promote { get; set; }
    }
}