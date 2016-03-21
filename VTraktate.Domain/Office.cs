using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Office : ISoftDelete, IEntity
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }
       
        public Provider Provider { get; set; }

        public virtual ICollection<CalendarPeriod> CalendarPeriods { get; set; }

        public OfficeTypes OfficeTypeId { get; set; }
        public OfficeType OfficeType { get; set;}

        public int LegalEntityID {get; set;}
        public LegalEntity LegalEntity { get; set; }

        public Boolean SharedEmployees { get; set; }

        public int? LegacyId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
