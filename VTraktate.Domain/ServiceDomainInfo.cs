using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTraktate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VTraktate.Domain
{
    public class ServiceDomainInfo : ITimeStamped, IEntity
    {
        public int Id { get; set; }
        public int DomainId { get; set; }
        public TranslationDomain Domain { get; set; }

        public QA QA { get; set; }

        public DateTime CreatedDate { get; set;}
        public int? CreatedById { get; set; }
        public Person CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedById { get; set; }
        public Person ModifiedBy { get; set; }

        public int LanguageId { get; set; }
        
        [ForeignKey("LanguageId")] 
        public ServiceLanguageInfo Language { get; set; }

        [InverseProperty("PrimaryDomainGraded")]
        public virtual ICollection<Grade> GradesAsPrimary { get; set; }

        [InverseProperty("SecondaryDomainGraded")]
        public virtual ICollection<Grade> GradesAsSecondary { get; set; }
    }
}
