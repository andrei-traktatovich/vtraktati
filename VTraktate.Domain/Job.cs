using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using VTraktate.Domain.ComplexTypes;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Job : IEntity, ITimeStamped, IVolumeAndPricing
    {
        public int Id { get; set; }

        #region volume and price
        public JobVolumeAndPricing Initial { get; set; }
        public JobVolumeAndPricing Final { get; set; }
        #endregion


        #region job properties


        public int JobTypeId { get; set; }
        [ForeignKey("JobTypeId")]
        public JobType JobType { get; set; }
        public int? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public LanguagePair Language { get; set; }
        public int UOMId { get; set; }
        public virtual JobUOM UOM { get; set; }
        
        public int? Domain1Id { get; set; }
        [ForeignKey("Domain1Id")]
        public TranslationDomain Domain1 { get; set; }
        
        public int? Domain2Id { get; set; }
        [ForeignKey("Domain2Id")]
        public TranslationDomain Domain2 { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public JobCompletionStatus Status { get; set; }

        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        
        public string Document { get; set; }
        #endregion

        #region ITimeStamped
        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public Person ModifiedBy { get; set; }

        #endregion

        #region ICompetableCalendarPeriod
        public DateTime StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? EndDate { get; set; }

        #endregion

        #region references and collections
        [InverseProperty("Job")]
        public ICollection<JobPart> JobParts { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int? DelegatedToOfficeId { get; set; }
        public Office DelegatedToOffice { get; set; }

        [InverseProperty("DaughterJob")]
        public JobPart ParentJobPart { get; set; }

        #endregion

        
    }

    




}
