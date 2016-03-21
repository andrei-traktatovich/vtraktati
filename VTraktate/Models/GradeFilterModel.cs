using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain;
using VTraktate.Filtering;

namespace VTraktate.Models
{
    public class GradeFilterModel : IFilterBindingModel<Grade>
    {

        public GradeFilterBindingModelFilter Filter { get; set; }

        public GradeFilterBindingModelSorting Sorting { get; set; }
        public int Page { get; set; }

        public int Count { get; set; }
    }

    public class GradeFilterBindingModelFilter : IFilterModel
    {
        public string Name { get; set; }
        public string JobPartName { get; set; }
        public string LangName { get; set; }
        public string DomainName { get; set; }
        public string Comment { get; set; }
        public string AuthoredBy { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class GradeFilterBindingModelSorting : ISortModel
    {
        [SourceProperty("CreatedDate")]
        public string CreatedDate { get; set; }

        [SourceProperty("Provider.Name")]
        public string Name { get; set; }
    }
}