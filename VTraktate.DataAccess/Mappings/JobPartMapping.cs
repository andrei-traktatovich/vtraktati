using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.DataAccess.Mappings
{
    class JobPartMapping : EntityTypeConfiguration<JobPart>
    {
        public JobPartMapping()
        {
            HasRequired(e => e.Job).WithMany(e => e.JobParts);
            HasRequired(e => e.JobType).WithMany(e => e.JobParts).WillCascadeOnDelete(false);
            HasRequired(e => e.UOM).WithMany(e => e.JobParts).WillCascadeOnDelete(false);
            HasRequired(e => e.Currency).WithMany(e => e.JobParts).WillCascadeOnDelete(false);
            HasOptional(e => e.DaughterJob).WithOptionalPrincipal(e => e.ParentJobPart);
        }
    }
}
