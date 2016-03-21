using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using VTraktate.Domain;
using VTraktate.Domain.Extensions;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Filtering
{
    using Expr = Expression<Func<AccountSnapshot,bool>>;
    public class AccountSnapshotFilteringRules : IFilteringRules 
    {
        public Expr Account(dynamic value)
        {
            var switcher = (bool)value;
            return p => p.AspNetUser != null;
        }
     
        public Expr UserName(dynamic value)
        {
            var userName = (string)value;
            if (!string.IsNullOrEmpty(userName))
                return p => p.AspNetUser != null & p.AspNetUser.UserName.Contains(userName);
            else
                return null;
        }

        public Expr Disabled(dynamic value)
        {
            var val = (bool?)value;
            return p => p.AspNetUser != null && p.AspNetUser.LoginDisabled == val;
        }
        
        public Expr InRole(dynamic value)
        {
            var roleName = (string)value;
            if (!string.IsNullOrEmpty(roleName))
                return p => p.AspNetUser != null && p.AspNetUser.AspNetRoles.Any(z => z.Name.Contains(roleName));
            return null; 
        }
        
        public Expr PersonName(dynamic value)
        {
            var personName = (string)value;
            if (!string.IsNullOrEmpty(personName))
                return p => p.Person.PersonName.FullName.Contains(personName);
            return null;
        }

        public Expr ProviderTypeID(dynamic value)
        {
            var id = (int?)value;
            if (id.HasValue)
                return p => p.ProviderSnapShot != null && p.ProviderSnapShot.Provider != null && (int)p.ProviderSnapShot.Provider.ProviderTypeId == id.Value;
            else
                return null;
        }

        public Expr PermOfficeID(dynamic value)
        {
            var id = ((int?)value);
            if (id.HasValue)
                return p => p.ProviderSnapShot != null && p.ProviderSnapShot.CurrentEmployment != null && p.ProviderSnapShot.CurrentEmployment.OfficeID == id;
            else
                return null;
        }

        public Expr TitleID(dynamic value)
        {
            var id = ((int?)value);
            if (id.HasValue)
                return p => p.ProviderSnapShot != null && p.ProviderSnapShot.CurrentEmployment != null && p.ProviderSnapShot.CurrentEmployment.TitleId == id;
            else
                return null;
        }

        public Expr EmplStatusID(dynamic value)
        {
            var id = ((int?)value);
            if (id.HasValue)
                return p => p.ProviderSnapShot != null && p.ProviderSnapShot.CurrentEmployment != null && p.ProviderSnapShot.CurrentEmployment.StatusID == id;
            else
                return null;
        }

        public Expr FreelanceStatusID(dynamic value)
        {
            throw new NotImplementedException("FreelanceStatusID");
        }

        public string DefaultSort
        {
            get { return "Name"; }
        }
    }
}
