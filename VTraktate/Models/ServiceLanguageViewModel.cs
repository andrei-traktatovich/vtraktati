using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class ServiceLanguageViewModel
    {
        public ServiceLanguageViewModel()
        {
            Domains = new List<ServiceDomainViewModel>();
        }
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int LanguagePairId { get; set; }
        public string LanguagePairName { get; set; }
        public decimal? QAGrade { get; set; }
        public int? QAStars { get; set; }
        public string QAComment { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }
        
        public decimal? ProductivityMin { get; set; }
        public decimal? ProductivityMax { get; set; }

        public string Comment { get; set; }
        public ICollection<ServiceDomainViewModel> Domains { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedByName { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByName { get; set; }

        public bool NativeSpeaker { get; set; }
    }
}