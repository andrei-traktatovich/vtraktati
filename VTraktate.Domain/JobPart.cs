using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.ComplexTypes;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class JobPart : ICompletableCalendarPeriod, IEntity, ITimeStamped, IVolumeAndPricing
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int StatusId { get; set; }
        public JobPartCompletionStatus Status { get; set; }

        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        public int JobTypeId { get; set; }
        [ForeignKey("JobTypeId")]
        public JobType JobType { get; set; }
        public int? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public LanguagePair Language { get; set; }

        public bool WorkInHouse { get; set; }
        public JobVolumeAndPricing Initial { get; set; }
        public JobVolumeAndPricing Final { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int UOMId { get; set; }
        public ServiceUOM UOM { get; set; }

        public virtual Job DaughterJob { get; set; }


        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedById { get; set; }

        public Person ModifiedBy { get; set; }
        
    }


    
}
