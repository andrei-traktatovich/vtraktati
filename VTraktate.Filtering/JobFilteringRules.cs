using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.Filtering;
using System.Linq.Expressions;
using VTraktate.Domain.Snapshots;
using VTraktate.Domain;

namespace VTraktate.Filtering
{
    using Expr = Expression<Func<Job, bool>>;
    public class JobFilteringRules : IFilteringRules
    {
        public Expr OfficeId(dynamic value)
        {
            var officeId = (int)value;
            if (officeId == -1)
                return x => x.Order.OfficeId == officeId && x.DelegatedToOfficeId == null;
            else
                return x => (x.Order.OfficeId == officeId && x.DelegatedToOfficeId == null) || x.DelegatedToOfficeId == officeId;
        }
        public string DefaultSort
        {
            get { return "Order.CreatedDate Desc"; }
        }
    }
}
