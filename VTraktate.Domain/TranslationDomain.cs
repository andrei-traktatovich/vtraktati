using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class TranslationDomain : ISoftDelete, ITimeStamped, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public TranslationDomain Parent { get; set; }



        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public Person ModifiedBy { get; set; }

        public int? LegacyId { get; set; }
    }
}
