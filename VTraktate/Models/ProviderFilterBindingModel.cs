using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain.Snapshots;
using VTraktate.Filtering;

namespace VTraktate.Models
{
    
    // ATTN : Why are you coupling filter model with underlying snapshot type? Any reason for that? 
    public class ProviderFilterBindingModel : IFilterBindingModel<ExtendedProviderSnapshot>
    {

        public ProviderFilterBindingModelFilter Filter { get; set; }

        public ProviderFilterBindingModelSorting Sorting { get; set; }
        public int Page { get; set; }

        public int Count { get; set; }
    }

    public class ProviderFilterBindingModelFilter : IFilterModel
    {
        public IEnumerable<int> ProviderTypes { get; set; }
        public IEnumerable<int> ServiceTypes { get; set; }
        public IEnumerable<int> Languages { get; set; }
        
        public IEnumerable<int> Titles { get; set; }
        public IEnumerable<int> Domains { get; set; }
        public IEnumerable<int> ProviderGroups { get; set; }
        public IEnumerable<int> Stars { get; set; }
        public IEnumerable<int> Uoms { get; set; }
        public IEnumerable<int> Currencies { get; set; }

        public IEnumerable<int> Offices { get; set; }

        public IEnumerable<int> EmplStatuses { get; set; }

        public IEnumerable<int> FreelanceStatuses { get; set; }

        public IEnumerable<int> Regions { get; set; }

        public IEnumerable<int> Soft { get; set; }

        public IEnumerable<int> AvailabilityStatuses { get; set; }
        
        [TerminalFilter]
        public string Name { get; set; }

        public bool? Native { get; set; }

        public IEnumerable<int> LegalForms { get; set; } 
        public bool? WorksNightly { get; set; }

        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }

        public int? MinGrade { get; set; }

        public int? MaxGrade { get; set; }
    }

    public class ProviderFilterBindingModelSorting : ISortModel
    {
        [SourceProperty("TypeName")]
        public string TypeName { get; set; }

        
        [SourceProperty("Name")]
        public string Name { get; set; }

        [SourceProperty("IsPromoted")]
        public string Promotion { get; set; }

        [SourceProperty("Rate.MinRate")]
        public string MinRate { get; set; }

        [SourceProperty("Rate.UomName")]
        public string UomName { get; set; }

        [SourceProperty("QA.Stars")]
        public string Stars { get; set; }

        [SourceProperty("QA.Grade")]
        public string Grade { get; set; }

    }

}