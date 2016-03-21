using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.ComplexTypes;
using VTraktate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTraktate.Domain
{
    public class Order : IEntity, ITimeStamped, ISoftDelete, IOptionallyNumbered
    {
        public int Id { get; set; }

        public int? Number { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }
        public string TransliterationRequirements { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int OfficeId { get; set; }
        public Office Office { get; set; }

        public ICollection<Job> Jobs { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedById { get; set; }

        public Person ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
