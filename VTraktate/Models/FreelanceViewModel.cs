using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class FreelanceViewModel
    {
        public IdNamePairBindingModel FreelanceStatus { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedByName { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByName { get; set; }

        public bool IsDeleted { get; set; } 
    }
}