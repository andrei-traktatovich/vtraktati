using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class ServiceLanguageInfo : ITimeStamped, IEntity
    {
        public ServiceLanguageInfo()
        {
            Domains = new List<ServiceDomainInfo>();
        }
        public int Id { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public int LanguagePairId { get; set; }
        public virtual LanguagePair LanguagePair { get; set; }

        public decimal? ProductivityMin { get; set; }
        public decimal? ProductivityMax { get; set; }

        public QA QA { get; set; }
        public ServiceRate Rate { get; set; }

        public string Comment { get; set; }
        public virtual ICollection<ServiceDomainInfo> Domains { get; set; }

        public bool NativeSpeaker { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public Person ModifiedBy { get; set; }
    }
}
