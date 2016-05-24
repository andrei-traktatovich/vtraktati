using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Models.Helpers;

namespace VTraktate.Models
{
    public class FreelanceCalendarPeriodViewModel
    {
        public int Id { get; set; }
        public IdNamePairBindingModel Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

     
}