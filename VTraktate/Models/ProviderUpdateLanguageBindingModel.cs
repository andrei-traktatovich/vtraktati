using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VTraktate.Domain;

namespace VTraktate.Models
{
    public class ProviderUpdateLanguageBindingModel
    {
        [Required]        
        public int Id { get; set; }
        
        [Required]
        public int LanguagePairId { get; set; }
        
        [Required]
        public int ServiceId { get; set; }
        
        [Required]
        public Stars qaStars { get; set; }
        
        [Required]
        public bool NativeSpeaker { get; set; }
        
        [Required]
        public decimal MinRate { get; set; }
        
        [Required]
        public decimal MaxRate { get; set; }
        
        public string Comment { get; set; }
        
        public string qaComment { get; set; }

        public decimal? ProductivityMin { get; set; }
        public decimal? ProductivityMax { get; set; }

    }
}