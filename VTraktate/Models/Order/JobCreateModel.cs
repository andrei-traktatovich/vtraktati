using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Order
{
    public class JobCreateModel
    {
        public JobVolumeAndPricingModel Initial { get; set; }
        
        public int? LanguageId { get; set; }

        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int JobTypeId { get; set; }
        public string Document { get; set; }
        public int? Domain1Id { get; set; }
        public int? Domain2Id { get; set; }
        
        [Required]
        public int UOMId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int OrderId { get; set; }
    }
}