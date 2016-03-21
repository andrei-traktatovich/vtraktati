using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class ServiceViewModel
    {
        public ServiceViewModel()
        {
            Languages = new List<ServiceLanguageViewModel>();
        }
        public int Id { get; set; }

        public int ProviderId { get; set; }

        
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public bool NeedsDomains { get; set; }
        public bool NeedsLanguage { get; set; }
        public int? QAStars { get; set; }
        public decimal? QAGrade { get; set; }
        public string QAComment { get; set; }

        public ICollection<ServiceLanguageViewModel> Languages { get; set; }

        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }

        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }

        public int ServiceUOMId { get; set; }
        public string ServiceUOMName { get; set; }


        public DateTime CreatedDate { get; set; }

        public string CreatedByName { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByName { get; set; }
    }
}