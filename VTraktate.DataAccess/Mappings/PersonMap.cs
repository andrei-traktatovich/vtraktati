using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.DataAccess.Mappings
{
    class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            HasRequired(x => x.CreatedBy).WithMany();
            HasOptional(x => x.ModifiedBy).WithMany();
            HasOptional(x => x.Provider).WithMany(x => x.ContactPersons).HasForeignKey(x => x.ProviderId);

            HasMany(e => e.AspNetUsers)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            HasMany(e => e.Customers)
                .WithMany(e => e.ContactPersons);

        }
    }
}
