using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Models.Order.ManagerOrderGrid
{
    public class JobFilterBindingModel : IFilterBindingModel<Job>
    {
        public int Page { get; set; }
        
        public int Count { get; set; }

        public JobFilterModel Filter { get; set; }

        public ISortModel Sort { get; set; }
    }

    public class JobFilterModel : IFilterModel
    {
        public int OfficeId { get; set; }
    }

    public class JobSortModel : ISortModel
    {

    }

}