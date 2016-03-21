using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class FireEmployeeBindingModel
    {
        
        public EmploymentBindingModel LastEmployment { get; set; }
        public FreelanceBindingModel Freelance { get; set; }
    }
}