using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Grade : ITimeStamped, ISoftDelete, IEntity
    {
        
        
        public int Id { get; set; }
        
        [Required]
        [Range(0, 10)]
        public int Score { get; set; }
        
        public string Comment { get; set; }
        public ErrorInfo Error {get; set;}
        public Bonus Bonus { get; set; }

        public JobPart JobPart {get; set;}
        public int? JobPartId { get; set; }
        
        // added for compatability with prior versions ...
        public string LegacyJobName { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }

        [ForeignKey("ServiceTypeId")]
        public ServiceType ServiceType { get; set; }
        public int ServiceTypeId { get; set; }

        [ForeignKey("LanguagePairId")]
        public LanguagePair LanguagePair { get; set; }
        public int? LanguagePairId { get; set; }

        [ForeignKey("PrimaryDomainId")]
        public TranslationDomain PrimaryDomain { get; set; }
        public int? PrimaryDomainId { get; set; }

        [ForeignKey("SecondaryDomainId")]
        public TranslationDomain SecondaryDomain { get; set; }
        public int? SecondaryDomainId { get; set; }


        [ForeignKey("ServiceGradedId")]
        public Service ServiceGraded { get; set; }
        public int? ServiceGradedId { get; set; }

        [ForeignKey("ServiceLanguageInfoGradedId")]
        public ServiceLanguageInfo ServiceLanguageInfoGraded { get; set; }
        public int? ServiceLanguageInfoGradedId { get; set; }


        [ForeignKey("PrimaryDomainGradedId")]
        public ServiceDomainInfo PrimaryDomainGraded { get; set; }
        public int? PrimaryDomainGradedId { get; set; }

        [ForeignKey("SecondaryDomainGradedId")]
        public ServiceDomainInfo SecondaryDomainGraded { get; set; }
        public int? SecondaryDomainGradedId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public Person ModifiedBy { get; set; }
        
    }

    public class Bonus
    {
        public bool NativeSpeaker { get; set; }
        public bool Quality { get; set; }
    }
    public class ErrorInfo
    {
        public bool Spelling { get; set; }
        public bool Fact { get; set; }
        public bool Term { get; set; }
        public bool Sense { get; set; }
        public bool Grammar { get; set; }
        public bool Omissions { get; set; }
        public bool Requirements { get; set; }
        public bool Style { get; set; }
    }
}
