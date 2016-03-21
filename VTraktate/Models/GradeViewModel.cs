using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using VTraktate.Domain;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Models
{
    public class GradeViewModel
    {
        public int Id { get; set; }
        public IdNamePairBindingModel Provider { get; set; }
        public decimal Score  { get; set; }

        public IdNamePairBindingModel JobPart { get; set; }
        public IdNamePairBindingModel Language { get; set; }
        public IdNamePairBindingModel Domain1 { get; set; }
        public IdNamePairBindingModel Domain2 { get; set; }

        public int? ServiceGradedId { get; set; }

        public int? ServiceLanguageInfoGradedId { get; set; }
        public int? PrimaryDomainGradedId { get; set; }
        public int? SecondaryDomainGradedId { get; set; }

        public GradeErrorDetailsModel Error { get; set; }
        public string Comment { get; set; }

        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        // TODO: this will have to be changed once I introduce real jobs into the equation !!! 
        public static Expression<Func<Grade, GradeViewModel>> FromGrade = x => new GradeViewModel 
        {
            Id = x.Id,
            Provider = new IdNamePairBindingModel {  Id = x.ProviderId, Name = x.Provider.Name },
            Score = x.Score,
            JobPart = new IdNamePairBindingModel { Id = -1, Name = x.LegacyJobName },           // only supporting legacy case 
            Language = x.LanguagePair != null ? new IdNamePairBindingModel {  Id = x.LanguagePair.Id, Name = x.LanguagePair.Name } : null,
            Domain1 = x.PrimaryDomain != null ? new IdNamePairBindingModel { Id = x.PrimaryDomain.Id, Name = x.PrimaryDomain.Name } : null,
            Domain2 = x.SecondaryDomain != null ? new IdNamePairBindingModel { Id = x.SecondaryDomain.Id, Name = x.SecondaryDomain.Name } : null,
            
            // TODO: complete this ... 
            Error = new GradeErrorDetailsModel(), 
            Comment = x.Comment,
            CreatedByName = x.CreatedBy.PersonName.FullName,
            CreatedDate = x.CreatedDate,
            ModifiedByName = x.ModifiedBy.PersonName.FullName,
            ModifiedDate = x.ModifiedDate,

            ServiceGradedId = x.ServiceGradedId,
            ServiceLanguageInfoGradedId = x.ServiceLanguageInfoGradedId,
            PrimaryDomainGradedId = x.PrimaryDomainGradedId,
            SecondaryDomainGradedId = x.SecondaryDomainGradedId
        };

    }

    public class GradeErrorDetailsModel
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

    public class GradeBonusDetailsModel
    {
        public bool Quality { get; set; }
        public bool Native { get; set; }
    }
}