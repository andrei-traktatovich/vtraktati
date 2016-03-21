using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.DataAccess.Mappings
{
    class EmploymentMap : EntityTypeConfiguration<Employment>
    {
        public EmploymentMap()
        {
            HasRequired(x => x.Provider).WithMany(x => x.Employments).HasForeignKey(x => x.ProviderId);
        }
    }
}

