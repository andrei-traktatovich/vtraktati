using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTraktate.Domain
{
    public class JobCompletionStatus : UniqueIdNamePair
    {
        public bool FinalVolumeRequired { get; set; }
    }

    public abstract class UniqueIdNamePair : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        [Index("IX_JobCompletionStatusName", 1, IsUnique = true)]
        public string Name { get; set; } // should name be unique? 
    }
}
