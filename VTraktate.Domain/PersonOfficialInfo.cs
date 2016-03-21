using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTraktate.Domain
{
    public class PersonOfficialInfo
    {
        
        [InverseProperty("Id")]
        [ForeignKey("Person")]
        [Key]
        public int PersonId { get; set; }
        
        public Person Person { get; set; }

        public string TitleName { get; set; }
        public string DepartmentName { get; set; }
        public string Comment { get; set; }
    }
}
