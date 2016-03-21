using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Core.Interfaces.Filtering
{
    public interface IFilterBindingModel<T>
        where T : class
    {
        int Page { get; set; }
        int Count { get; set; }

    }

    public interface IFilterModel
    { }

    public interface ISortModel
    { }

    public interface IFilteringRules 
    {
        string DefaultSort { get; }
    }
}
