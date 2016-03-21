using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class EmailViewModel
    {
        public int Id { get; set; }
        public int ContactPersonId { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }
    }
}