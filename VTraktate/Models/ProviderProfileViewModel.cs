using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using VTraktate.Domain;
using System.Data.Entity;

namespace VTraktate.Models
{
    public class ProviderProfileViewModel
    {
        public ProviderProfileViewModel()
        {
            ContactPersons = new List<ContactPersonViewModel>();
            Services = new List<ServiceViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string City { get; set; }
        public string Address { get; set; }

        public decimal TimeDifference { get; set; }
        public bool WorksNightly { get; set; }
        public IdNamePairBindingModel LegalForm { get; set; }

        public IdNamePairBindingModel Region { get; set; }

        public ICollection<ContactPersonViewModel> ContactPersons { get; set; }
        public ICollection<ServiceViewModel> Services { get; set; }
        public ICollection<EmploymentViewModel> Employments { get; set; }

        public ICollection<IdNamePairBindingModel> Soft { get; set; }
        public ICollection<FreelanceViewModel> Freelances { get; set; }
        public bool IsDeleted { get; set; }

        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}