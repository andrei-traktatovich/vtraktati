using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTraktate.Domain;
using VTraktate.Domain.ComplexTypes;

namespace VTraktate.Models
{
    public class ContactPersonViewModelBase
    {
        public ContactPersonViewModelBase()
        {
            Emails = new List<EmailViewModel>();
            Phones = new List<PhoneViewModel>();
        }

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string FullName 
        { 
            get { return PersonName.FullName; } 
        }

        public IndividualName PersonName { get; set; }
        public string Comment { get; set; }
        public ICollection<EmailViewModel> Emails { get; set; }

        public ICollection<OtherContactViewModel> OtherContacts { get; set; }

        public ICollection<PhoneViewModel> Phones { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedByName { get; set; }
    }
    public class ContactPersonViewModel : ContactPersonViewModelBase
    {
        public int? ProviderId { get; set; }
    }

    public class CustomerContactViewModel : ContactPersonViewModelBase
    {
        public string TitleName { get; set; }
        public string DepartmentName { get; set; }
    }
}
