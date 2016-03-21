using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VTraktate.Domain;

namespace VTraktate.Models
{
    public class ProviderDomainUpdateBindingModel
    {
        [Required]
        public Stars Stars { get; set; }
        public string Comment { get; set; }
    }
}