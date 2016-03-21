using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Service : ITimeStamped, IEntity
    {
        public Service()
        {
            Languages = new List<ServiceLanguageInfo>();
        }
        public int Id { get; set; }

        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }

        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        
        public QA QA { get; set; }

        public virtual ICollection<ServiceLanguageInfo> Languages { get; set; }

        public ServiceRate Rate { get; set; }

        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public int ServiceUOMId { get; set; }
        public ServiceUOM ServiceUOM { get; set; }


        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public Person ModifiedBy { get; set; }

        public int? LegacyId { get; set; }

        [InverseProperty("ServiceGraded")]
        public virtual ICollection<Grade> Grades {get; set;}
    }
}
