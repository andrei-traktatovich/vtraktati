using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces;
using VTraktate.Domain;

namespace VTraktate.BL.Cerberos 
{
    public class Cerberos : ICerberos
    {
        public ITraktatContext Context { get; private set; }
        public int UserId { get; private set; }

        public Cerberos(ITraktatContext context, int userId)
        {
            Context = context;
            if(userId == 0)
                throw new ArgumentException("userId");

            UserId = userId;
        }

        private AspNetUser _user;

        public async Task<AspNetUser> GetUserAsync()
        {
            if (_user != null)
                return _user;
            _user = await Context.GetByIdAsync<AspNetUser>(UserId);
            if(EntityUtils.IsNull(_user))
                throw new InvalidOperationException();
            return _user;
        }

        public async Task<bool> CanDeleteProvidersAsync()
        {
            var user = await GetUserAsync();
            if (EntityUtils.IsNull(user))
                return false;

            return user.IsInRole(AspNetRole.RolesToDeleteProvider);
        }
    }

    public static class AspNetUserExtensions
    {
        public static bool IsInRole(this AspNetUser @this, params int[] roleIds)
        {
            return @this.AspNetRoles != null && @this.AspNetRoles.Any(x => roleIds.Contains(x.Id));
        }
    }
}
