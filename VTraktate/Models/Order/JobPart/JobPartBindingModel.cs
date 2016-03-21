using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Order.JobPart
{
    public class JobPartCreateBindingModel : JobPartBindingModelBase
    {

    }
    public class JobPartEditBindingModel : JobPartBindingModelBase
    {
        [Required]
        public int Id { get; set; }

        public JobVolumeAndPricingModel Final { get; set; }
    }
    public class JobPartBindingModelBase
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public int JobTypeId { get; set; }
        public int? LanguageId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int UOMId { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        
        public string Comment { get; set; }
        public JobVolumeAndPricingModel Initial { get; set; }
        
        
        [Required]
        public bool InHouse { get; set; }
    }
}