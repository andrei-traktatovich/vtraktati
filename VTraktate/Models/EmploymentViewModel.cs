using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Core.Infrastructure;
using VTraktate.Models.Helpers;

namespace VTraktate.Models
{
    public class EmploymentViewModel
    {
        public IdNamePairBindingModel Status { get; set; }
        public IdNamePairBindingModel Title { get; set; }
        public IdNamePairBindingModel Office { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string Comment { get; set; }

        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}