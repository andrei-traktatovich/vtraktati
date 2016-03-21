using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class OtherContactViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public IdNamePairBindingModel Type { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }

        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}