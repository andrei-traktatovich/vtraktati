using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class ServiceDomainViewModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int DomainId { get; set; }
        public string DomainName { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? Stars { get; set; }
        public decimal? Grade { get; set; }
        public string Comment { get; set; }
    }
}