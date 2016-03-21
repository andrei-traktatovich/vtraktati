using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System;
using System.Web;

namespace VTraktate.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie); // TODO: will need to change this to enable "transient" roles 
            return userIdentity;
        }

        public bool LoginDisabled { get; set; }

        public int PersonId { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("IdentityDbContext") { }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationUserRole : IdentityUserRole<int> { } 

    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>, IRole<int> 
    {
        public string Description { get; set; }
        public ApplicationRole() { }
        public ApplicationRole(string name) : this()
        {
            this.Name = name;
        }
        public ApplicationRole(string name, string description) : this(name)
        {
            this.Description = description;
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, 
        int, ApplicationUserLogin, ApplicationUserRole, 
        ApplicationUserClaim>, 
        IUserStore<ApplicationUser, int>, 
        IDisposable
    {
        public ApplicationUserStore() : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationUserStore(DbContext context) : base(context) { }
    }

    public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>,
        IQueryableRoleStore<ApplicationRole, int>,
        IRoleStore<ApplicationRole, int>, 
        IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext()) 
        { 
            base.DisposeContext = true;
        }

        public ApplicationRoleStore(DbContext context) : base(context) { }

    }

    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //InitializeIdentityForEF(context);
            //base.Seed(context);
        }

        public static void InitializeIdentityForEF(ApplicationDbContext context)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            const string name = "admin";
            const string password = "Admin@123456";
            const string roleName = "Администратор";
            const string email = "nikolaev@traktat.com";

            // Create role ADMIN if not existent
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new ApplicationRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = email };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
                
        }

    }
}

 