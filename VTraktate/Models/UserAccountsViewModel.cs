using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using VTraktate.Core.Infrastructure;
using VTraktate.Domain;
using VTraktate.Domain.Extensions;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Models
{
    public class UserAccountsViewModel
    {
        public int PersonId { get; set; }
        public string Name { get; set; }

        public string TypeName { get; set; }
        public string FreelanceStatus { get; set; }

        public UserEmployment Employment { get; set; }
        public UserAccount Account { get; set; }
        public static Expression<Func<AccountSnapshot, UserAccountsViewModel>> FromAccountSnapShot
        {
            get
            {
                return x => new UserAccountsViewModel
                {
                    PersonId = x.Person.Id,
                    Name = x.Person.PersonName.FullName,
                    TypeName = x.ProviderSnapShot != null & x.ProviderSnapShot.Provider != null ? x.ProviderSnapShot.Provider.ProviderType.Name : null,
                    FreelanceStatus = x.ProviderSnapShot.CurrentFreelance != null ? x.ProviderSnapShot.CurrentFreelance.FreelanceStatus.Name : null,
                    
                    Employment = (x.ProviderSnapShot != null && x.ProviderSnapShot.CurrentEmployment != null) ? new UserEmployment
                    {
                        CurrentOfficeName = x.ProviderSnapShot.CurrentEmployment.Office.Name,
                        EmploymentStatus = x.ProviderSnapShot.CurrentEmployment.Status.Name,
                        PermanentOfficeName = x.ProviderSnapShot.CurrentEmployment.Office.Name,
                        TitleName = x.ProviderSnapShot.CurrentEmployment.Title.Name
                    } : null,
                    
                    Account = x.AspNetUser != null ? new UserAccount
                    {
                        AccountDisabled = x.AspNetUser.LoginDisabled,
                        AccountId = x.AspNetUser.Id,
                        AccountName = x.AspNetUser.UserName,
                        Roles = x.AspNetUser.AspNetRoles.Select(role => new IdTitlePair { Id = role.Id, Title = role.Name })
                    } : null
                };
            }
        }

        public class UserAccount
        {
            public int AccountId { get; set; }
            public string AccountName { get; set; }

            public string Email { get; set; }
            public IEnumerable<IdTitlePair> Roles { get; set; }
            public bool AccountDisabled { get; set; }
        }
        public class UserEmployment
        {
            public string PermanentOfficeName { get; set; }
            public string CurrentOfficeName { get; set; }
            public string TitleName { get; set; }
            public string EmploymentStatus { get; set; }
        }
   
    }
}