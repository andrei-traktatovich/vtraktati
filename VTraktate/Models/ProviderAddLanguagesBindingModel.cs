using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class ProviderAddLanguagesBindingModel
    {
        [MinLength(1)]
        public int[] Languages { get; set; }
        public int Stars { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal MinRate { get; set; }
        
        [Required]
        [Range(0, 100000)]
        public decimal MaxRate { get; set; }
        public string Comment { get; set; }
        public string qaComment { get; set; }
        public bool NativeSpeaker { get; set; }

        public decimal? ProductivityMin { get; set; }
        public decimal? ProductivityMax { get; set; }
    }
}