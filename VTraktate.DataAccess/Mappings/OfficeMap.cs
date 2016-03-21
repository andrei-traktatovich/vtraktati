using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.DataAccess.Mappings
{
    class OfficeMap : EntityTypeConfiguration<Office>
    {
        public OfficeMap()
        {
            HasOptional(x => x.Provider).WithOptionalPrincipal(x => x.Office);
        }
    }
}
