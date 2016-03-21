using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain;

namespace VTraktate.Filtering
{
    // TODO: This only supports legacy use case (grades independent of orders and jobparts

    using Expr = Expression<Func<Grade, bool>>;
    public class GradeFilteringRules : IFilteringRules 
    {
        public string DefaultSort
        {
            get { return "CreatedDate Desc"; }
        }

        public Expr Name (dynamic value)
        {
            var str = (string)value;
            if (!string.IsNullOrEmpty(str))
                return x => x.Provider.Name.Contains(str);
            else 
                return null;
        }

        public Expr JobPartName (dynamic value)
        {
            var str = (string)value;
            if (!string.IsNullOrEmpty(str))
                return x => (x.JobPart == null && x.LegacyJobName.Contains(str)); // TODO: add new scenario ...
            else
                return null;
        }
        public Expr LangName(dynamic value)
        {
            var str = (string)value;
            if (!string.IsNullOrEmpty(str))
                return x => x.LanguagePair != null && x.LanguagePair.Name.Contains(str);
            else 
                return null;
        }

        public Expr DomainName(dynamic value)
        {
            var str = (string)value;
            if (!string.IsNullOrEmpty(str))
                return x => (x.PrimaryDomain != null && x.PrimaryDomain.Name.Contains(str)) || (x.SecondaryDomain != null && x.SecondaryDomain.Name.Contains(str));
            else 
                return null;
        }

        public Expr Comment(dynamic value)
        {
            var str = (string)value;
            if (!string.IsNullOrEmpty(str))
                return x => x.Comment.Contains(str);
            else 
                return null;
        }

        public Expr AuthoredBy(dynamic value)
        {
            var str = (string)value;
            if (!string.IsNullOrEmpty(str))
                return x => x.CreatedBy.PersonName.FullName.Contains(str) || x.ModifiedBy.PersonName.FullName.Contains(str);
            else
                return null;
        }

        public Expr StartDate(dynamic value)
        {
            if (value is DateTime) {
                var dt = ((DateTime)value).ToLocalTime();
                if (dt != null)
                    return x => x.CreatedDate >= dt;
            }
            return null;
        }

        public Expr EndDate(dynamic value)
        {
            if (value is DateTime)
            {
                var dt = ((DateTime)value).ToLocalTime();
                if (dt != null)
                    return x => x.CreatedDate <= dt;
            }
            return null;
        }
    }
}
