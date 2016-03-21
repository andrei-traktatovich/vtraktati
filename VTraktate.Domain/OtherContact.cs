using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class OtherContact : ITimeStamped, ISoftDelete, IEntity
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int TypeId { get; set; }
        public OtherContactType Type { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }

        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        [InverseProperty("OtherContacts")]
        public Person Person { get; set; }

        public DateTime CreatedDate { get; set; }
        public int? CreatedById { get; set; }
        
        [ForeignKey("CreatedById")]
        public Person CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedById { get; set; }
        [ForeignKey("ModifiedById")]
        public Person ModifiedBy { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
