using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Title : ISoftDelete, ITimeStamped, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Person ModifiedBy { get; set; }


        [ForeignKey("CreatedBy")]
        public int? CreatedById { get; set; }

        [ForeignKey("ModifiedBy")]
        public int? ModifiedById { get; set; }

        public int? LegacyId { get; set; }
        
    }
}
