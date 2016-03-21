using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTraktate.Domain
{
    public class JobUOM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index("IX_JobUOMName", 1, IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
