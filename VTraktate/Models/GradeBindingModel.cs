using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class LegacyGradeBindingModel
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string LegacyJobName { get; set; }
        public int Score { get; set; }
        public int LanguageId { get; set; }
        public int Domain1Id { get; set; }
        public int? Domain2Id { get; set; }
        public string Comment { get; set; }
        public int? ServiceTypeId { get; set; }
        public GradeErrorDetailsModel Error { get; set; }

        public GradeBonusDetailsModel Bonus { get; set; }
    }

    
}