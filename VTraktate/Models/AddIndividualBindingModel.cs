using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Domain;

namespace VTraktate.Models
{

    public class IndividualProviderModel
    {
        // make this one universal so that parts can be used to patch parts 
        // of the graph and the whole stuff can be used to 
        // patch the provider
        public IdNamePairBindingModel Type { get; set; }
        public PersonExtendedNameBindingModel PersonName { get; set; }
        public IndividualDetailsBindingModel Details { get; set; }
        public FreelanceBindingModel Freelance { get; set; }
        public EmploymentBindingModel Employment { get; set; }
        public EmailBindingModel[] Emails { get; set; }
        public PhoneBindingModel[] Telephones { get; set; }
        public OtherContactsBindingModel[] OtherContacts { get; set; }
        public ServiceBindingModel[] Services { get; set; }
        public bool Promote { get; set; }
    }

    public class IdNamePairBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

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

    public class FreelanceBindingModel
    {
        public int StatusId { get; set; }
        public string Comment { get; set; }
    }

    public class EmploymentBindingModel
    {
        public int StatusId { get; set; }
        public int TitleId { get; set; }
        public int OfficeId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string Comment { get; set; }
    }

    

    public class PhoneBindingModel
    {
        public string Phone { get; set; }
        public string Ext { get; set; }
        public bool Active { get; set; }
        public int TypeId { get; set; }
        public string Comment { get; set; }
    }


    public class OtherContactsBindingModel
    {
        public string Address { get; set; }
        public int Type { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }
    }

    public class ServiceBindingModel
    {
        public IdNamePairBindingModel Type { get; set; }
        public int Stars { get; set; }

        public string Comment { get; set; }
        public int CurrencyId { get; set; }
        public int UomId { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }
        public bool IsLinguistic { get; set; }
        public LanguageBindingModel[] Languages { get; set; }

    }

    
    public class LanguageBindingModel
    {
        public IdNamePairBindingModel LanguagePair { get; set; }
        public int Stars { get; set; }
        public DomainBindingModel[] Domains { get; set; }
        public bool Native { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }
        public string Comment { get; set; }

        public decimal? ProductivityMin { get; set; }
        public decimal? ProductivityMax { get; set; }
    }

    public class DomainBindingModel
    {
        public IdNamePairBindingModel Domain { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}