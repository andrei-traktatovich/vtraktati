using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTraktate.Domain
{
    public class JobType : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index("IX_JobTypeName", 1, IsUnique = true)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Index("IX_JobTypeLongName", 1, IsUnique = true)]
        public string LongName { get; set; }
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public bool IsInternal { get; set; }
        public int UomId { get; set; }
        public virtual JobUOM UOM { get; set; }

        public virtual ICollection<JobPart> JobParts { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
