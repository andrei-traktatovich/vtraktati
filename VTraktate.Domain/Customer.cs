using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTraktate.Domain
{
    public class Customer : IEntity, ISoftDelete, ITimeStamped
    {
        public int Id { get; set; }

        public bool Protected { get; set; }
        public bool Hidden { get; set; }

        public bool IsIndividual { get; set; }
        public bool NumberPerOffice { get; set; }

        [Required]
        [MaxLength(500)]
        public string LongName { get; set; }

        [Required]
        [MaxLength(500)]
        [Index("IX_CustomerShortName", 1, IsUnique = true)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(20)]
        [Index("IX_CustomerCode", 1, IsUnique = true)]
        public string Code { get; set; }

        public ICollection<Person> ContactPersons { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public Person ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public bool UseOfficeCode { get; set; }

        public int RoundingPolicyId { get; set; }
        public RoundingPolicy RoundingPolicy { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
