using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Filtering
{
    using Expr = Expression<Func<ExtendedProviderSnapshot, bool>>;
    public class ExtendedProviderSnapshotFilteringRules : IFilteringRules
    {

        public Expr ServiceTypes(dynamic value)
        {
            var val = value as IEnumerable<int>;

            if (val != null)
                return x => val.Contains(x.Service.ServiceTypeId);
            else
                return null;
        }

        public Expr Offices(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => val.Contains(x.Employment.OfficeID); // x.Employment != null && 
            else
                return null;
        }

        public Expr ProviderTypes(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => val.Contains((int)x.Provider.ProviderTypeId);
            else
                return null;
        }

        public Expr EmplStatuses(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => (x.Employment == null) || (val.Contains(x.Employment.StatusID));
            else
                return null;
        }

        public Expr LegalForms(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => val.Contains(x.Provider.LegalFormId);
            else
                return null;
        }

        public Expr WorksNightly(dynamic value)
        {
            var val = value as bool?;
            if (val.HasValue && val.Value)
                return x => x.Provider.WorksNightly;
            else
                return null;
        }

        public Expr Titles(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => (x.Employment == null) || (val.Contains(x.Employment.TitleId));
            else
                return null;
        }

        public Expr FreelanceStatuses(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => (x.Freelance != null) && (val.Contains(x.Freelance.FreelanceStatusID));
            else
                return null;
        }

        public Expr Regions(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => val.Contains(x.Provider.RegionId);
            else
                return null;
        }

        public Expr MinRate(dynamic value)
        {
            var val = (decimal)value;
            if (val != null)
                return x => (x.Service.ServiceType.SpecifyLanguage && x.Language.Rate.Minrate >= val)
                    || x.Service.Rate.Minrate >= val;
            else
                return null;
        }

        public Expr MaxRate(dynamic value)
        {
            var val = (decimal)value;
            if (val != null)
                return x => (x.Service.ServiceType.SpecifyLanguage && x.Language.Rate.MaxRate <= val)
                    || x.Service.Rate.Minrate <= val;
            else
                return null;
        }

        public Expr Uoms(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => val.Contains(x.Service.ServiceUOMId);
            else
                return null;
        }

        public Expr Currencies(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => val.Contains(x.Service.CurrencyId);
            else
                return null;
        }

        public Expr Stars(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => x.Language != null && val.Contains((int)x.Language.QA.Stars) || val.Contains((int)x.Service.QA.Stars);
            else
                return null;
        }

        public Expr MinGrade(dynamic value)
        {
            var val = (decimal)value;
            if (val != null)
                return x => x.Language != null && x.Language.QA.Grade >= val || x.Service.QA.Grade >= val;
            else
                return null;
        }

        public Expr MaxGrade(dynamic value)
        {
            var val = (decimal)value;
            if (val != null)
                return x => x.Language != null && x.Language.QA.Grade <= val || x.Service.QA.Grade <= val;
            else
                return null;
        }

        public Expr Languages(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => x.Language != null && val.Contains(x.Language.LanguagePairId);
            else
                return null;
        }

        public Expr Domains(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => x.Language != null && x.Language.Domains.Any(y => val.Contains(y.DomainId));
            else
                return null;
        }

        public Expr AvailabilityStatuses(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => val.Contains(x.AvailabilityStatusId);
            else
                return null;
        }

        public Expr Name(dynamic value)
        {
            var val = (string)value;
            if (val != null)
                return x => (x.Provider.Name.Contains(val));
            else
                return null;
        }

        public Expr Soft(dynamic value)
        {
            var val = value as IEnumerable<int>;
            if (val != null)
                return x => x.Provider.Soft.Any(y => val.Contains(y.Id));
            else
                return null;
        }

        public Expr Native(dynamic value)
        {
            if (value != null && value as bool? != null)
            {
                var val = value as bool?;
                if (val != null)
                    return x => x.Language.NativeSpeaker == val.Value;
            }
            return null;
        }

        
        public string DefaultSort
        {
            get { return "Name"; }
        }
    }
}
