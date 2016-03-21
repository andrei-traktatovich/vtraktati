using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain;
using VTraktate.Domain.Snapshots;
namespace VTraktate.Models
{

    public class AccountFilterBindingModel : IFilterBindingModel<AccountSnapshot>
    {
        public int Page { get; set; }

        public int Count { get; set; }

        public UserAccountFilterModel Filter { get; set; }

        public UserAccountSortingModel Sorting { get; set; }

    }

    public class UserAccountSortingModel : ISortModel
    {

    }
    public class UserAccountFilterModel : IFilterModel
    {
        public bool? Account { get; set; }          // has account ? 
        public string UserName { get; set; }        // account name contains 
        public bool? Disabled { get; set; }         // account disabled ? 
        public string InRole { get; set; }          // has a role with name containing ...
        public string PersonName { get; set; }      // Person's Name contains ...
        public int? ProviderTypeID { get; set; }    // provider has type ...
        public int? PermOfficeID { get; set; }       // permanent office
        public int? TitleID { get; set; }           // has title ...?
        public int? EmplStatusID { get; set; }      // employee has employmentStatus ... ? 
        public int? FreelanceStatusID { get; set; } // freelancer has status ... ? 


    }
}