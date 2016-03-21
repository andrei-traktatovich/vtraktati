using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.DataAccess.Mappings
{
    class JobMapping : EntityTypeConfiguration<Job>
    {
        public JobMapping()
        {
            HasRequired(e => e.UOM).WithMany(e => e.Jobs).WillCascadeOnDelete(false);
        }
    }
}
