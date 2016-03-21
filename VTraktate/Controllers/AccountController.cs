using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using VTraktate.Models;
using VTraktate.Providers;
using VTraktate.Results;
using System.Linq;
using System.Data.Entity;
using VTraktate.Filtering;
using VTraktate.DataAccess;
using VTraktate.Domain;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Core.Interfaces;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ISnapshotProvider<AccountSnapshot> _snapshotProvider;
        private IQueryFilterService<AccountSnapshot> _queryFilterService;
        private IGlobalsProvider _globalsProvider;
        public AccountController(ISnapshotProvider<AccountSnapshot> snapshotProvider, 
            IQueryFilterService<AccountSnapshot> queryFilterService, 
            IGlobalsProvider globalsProvider)
        {
            _snapshotProvider = snapshotProvider;
            _queryFilterService = queryFilterService;
            _globalsProvider = globalsProvider;
        }

        // ATTN: I don't have an idea what to do with this constructor and whether it is used anywhere or is required by anything
        //public AccountController(ApplicationUserManager userManager,
        //    ISecureDataFormat<AuthenticationTicket> accessTokenFormat, ApplicationRoleManager roleManager)
        //{
        //    UserManager = userManager;
        //    AccessTokenFormat = accessTokenFormat;
        //    _roleManager = roleManager;
        //}

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null) 
                    _roleManager  = Request.GetOwinContext().Get<ApplicationRoleManager>();
                return _roleManager;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return _userManager;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/Search?params 
        [Route("Search")]
        public async Task<IHttpActionResult> GetAccounts([FromUri] AccountFilterBindingModel search)
        {
            var accounts = _snapshotProvider.Get();

            var result = await _queryFilterService.GetFilteredOrderedPageAsync<UserAccountsViewModel>(

                accounts,
                search.Filter,
                search.Sorting,
                search.Page,
                search.Count,
                UserAccountsViewModel.FromAccountSnapShot, 
                useDistinctAfterProjection: false
                );
             
            return Ok(result);
          
        }

        [Route("Profile")]
        public async Task<IHttpActionResult> GetProfile()
        {
            //var ctx = new TraktatData();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (user == null)
                return BadRequest("Пользователь не найден.");
            
            var account = await _snapshotProvider.GetByRootIdAsync(user.Id);

            if (account == null)
                return BadRequest("Профиль пользователя не найден");

            var profile = new CurrentUserProfileModel(account);

            var globals = _globalsProvider.Get();

            var result = new { User = profile, Globals = globals };
            return Ok(result);
            
            
        }

         
        [Route("Unique")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckUniqueAccountName([FromBody] NamePreviewBindingModel model)
        {
            var name = model.Value.Trim();
            var user = await UserManager.FindByNameAsync(name);
            var exists = user != null && (!model.Id.HasValue || model.Id.Value != user.Id);

            var error = exists ? string.Format("Имя пользователя {0} не является уникальным.", name) : null;

            return Ok(new { Ok = !exists, ErrorMessage = error });
        }
        
        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
            

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (ApplicationUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await SetPasswordAsync(User.Identity.GetUserId<int>(), model.NewPassword);
        }

        private async Task<IHttpActionResult> SetPasswordAsync(int userID, string firstPassword)
        {
            IdentityResult result = await UserManager.AddPasswordAsync(userID, firstPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }


        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId<int>(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId<int>());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId<int>(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                
                 ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }


        // PUT api/account {}
        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> UpdateAccount(UpdateAccountBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Roles.Length == 0)
            {
                return BadRequest("Нет ни одной роли. Невозможно создать учетную запись без ролей.");
            }

            // find user 
            var user = await UserManager.FindByIdAsync(model.AccountId);
            if (user == null)
                return NotFound();
            // update user 
            user.LoginDisabled = model.LoginDisabled;
            user.Email = model.Email;

            IdentityResult userUpdate = await UserManager.UpdateAsync(user);
            if (!userUpdate.Succeeded)
                return GetErrorResult(userUpdate);

            
            var roles = RoleManager.Roles;
            var currentUserRoles = user.Roles.Select(x => x.RoleId);
            var currentRoles = new List<string>();
            foreach (var currentRoleId in currentUserRoles)
            {
                var roleName = RoleManager.FindById(currentRoleId).Name;
                if (!string.IsNullOrEmpty(roleName))
                    currentRoles.Add(roleName);
            }

            var rolesToRemove = currentRoles.Except(model.Roles).ToArray();
            var rolesToAdd = model.Roles.Except(currentRoles).ToArray();

            var roleAddition = await UserManager.AddToRolesAsync(user.Id, rolesToAdd);
            var roleRemoval = await UserManager.RemoveFromRolesAsync(user.Id, rolesToRemove);

            if (!roleAddition.Succeeded)
                return GetErrorResult(roleAddition);
            if (!roleRemoval.Succeeded)
                return GetErrorResult(roleRemoval);

            // password
            if (!string.IsNullOrEmpty(model.Password))
            {
                var passwordChange = await UserManager.ForceNewPasswordAsync(user, model.Password);
                if (!passwordChange.Succeeded)
                    return GetErrorResult(passwordChange);
            }

            return Ok();
        }
        
        // POST api/account {RegisterPersonBindingModel}
        [HttpPost]
        [Authorize(Roles="администратор")]
        public async Task<IHttpActionResult> CreateAccount(RegisterPersonBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Roles.Length == 0)
            {
                return BadRequest("Нет ни одной роли. Невозможно создать учетную запись без ролей.");
            }

            if (model.PersonId == null)
            {
                return BadRequest("Не указано, какому пользователю сопоставить учетную запись.");
            }
            
            // creating user 
            var user = new ApplicationUser() { UserName = model.Name, Email = model.Email, PersonId = model.PersonId.Value, LoginDisabled = false };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            // adding roles to the user 
            if (model.Roles.Length == 0)
            {
                return BadRequest("Пользователь создан, однако у него нет ролей. Пользователь не сможет войти в систему.");
            }

            IdentityResult rolesResult = await UserManager.AddToRolesAsync(user.Id, model.Roles);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            throw new NotSupportedException("Самостоятельная регистрация не поддерживается в этой версии");
        }

        [HttpPut]
        [Route("~/api/account/{id:int}/disable")]
        
        public async Task<IHttpActionResult> DisableAccount(int id, [FromUri] bool val)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            user.LoginDisabled = val;
            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok();
            else
                return GetErrorResult(result);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest("Не указан ID пользователя");

            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            // if user can't be deleted, throw error

            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok();
            else
                return GetErrorResult(result);
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result); 
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
