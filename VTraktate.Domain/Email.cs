using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.ComplexTypes;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Email : ISoftDelete, ITimeStamped, IEntity
    {
        
        public int Id { get; set; }
        public EmailAddress Address { get; set; }

        
        public int ContactPersonId { get; set; }

        [InverseProperty("Emails")]
        [ForeignKey("ContactPersonId")]
        public Person ContactPerson { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set ; }
        
        public int? ModifiedById { get; set; }
        
        [ForeignKey("ModifiedById")]
        public Person ModifiedBy { get; set; }
    }
}
