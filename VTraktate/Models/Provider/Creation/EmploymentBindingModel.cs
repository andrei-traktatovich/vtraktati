using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Provider.Creation
{
    public class EmploymentBindingModel
    {
        public int StatusId { get; set; }
        public int TitleId { get; set; }
        public int OfficeId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string Comment { get; set; }
    }
}