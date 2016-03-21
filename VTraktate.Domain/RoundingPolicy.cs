using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTraktate.Domain
{
    public class RoundingPolicy : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index("IX_RoundingPolicyName", 1, IsUnique = true)]
        public string Name { get; set; }
    }
}
