using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class PhoneViewModel : PhoneBindingModel
    {
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Id { get; set; }
        public string TypeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}