using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using VTraktate.Domain;
using VTraktate.Domain.ComplexTypes;
using VTraktate.Domain.Snapshots;
using VTraktate.BL.ExtensionMethods.ProviderSnapshotExtensions;

namespace VTraktate.Models
{
    public class CurrentUserProfileModel
    {
        public CurrentUserProfileModel(AccountSnapshot account)
        {
            // this class will be extended if need should arise ... 
            this.Roles = account.AspNetUser.AspNetRoles.Select(x => x.Id).ToArray();
            this.Naming = account.Person.PersonName;
            this.Id = account.Person.Id;
            this.OfficeId = account.ProviderSnapShot != null ? account.ProviderSnapShot.GetCurrentOfficeId() : null;
        }
        public int[] Roles { get; private set; }
        public IndividualName Naming { get; private set; }
        public int? OfficeId { get; set; }
        public int Id { get; private set; }
    }
}