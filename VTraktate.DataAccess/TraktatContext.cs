namespace VTraktate.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using VTraktate.DataAccess.Mappings;
    using VTraktate.Domain.ComplexTypes;
    using VTraktate.Domain;
    using VTraktate.Domain.Interfaces;
    using System.Data.Entity.Infrastructure;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Data.Entity.Validation;

    public partial class TraktatContext : DbContext
    {
        public TraktatContext()
            : base("name=TraktatContext")
        {
        }

        public virtual IDbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual IDbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual IDbSet<Person> People { get; set; }
        public virtual IDbSet<Provider> Providers { get; set; }
        public virtual IDbSet<Office> Offices { get; set; }

        public virtual IDbSet<Employment> Employments { get; set; }
        public virtual IDbSet<EmploymentStatus> EmploymentStatuses { get; set; }
        public virtual IDbSet<Title> Titles { get; set; }

        public virtual IDbSet<Freelance> Freelances { get; set; }
        public virtual IDbSet<FreelanceStatus> FreelanceStatuses { get; set; }

        public virtual IDbSet<Region> Regions { get; set; }
        public virtual IDbSet<Email> Emails { get; set; }

        public virtual IDbSet<ProviderType> ProviderTypes { get; set; }

        public virtual IDbSet<CalendarPeriod> CalendarPeriods { get; set; }
        public virtual IDbSet<EmployeeCalendarStatus> EmployeeCalendarStatuses { get; set; }

        public virtual IDbSet<Service> Services { get; set; }

        public virtual IDbSet<ServiceType> ServiceTypes { get; set; }
        public virtual IDbSet<ServiceLanguageInfo> ServiceLanguageInfos { get; set; }
        public virtual IDbSet<ServiceDomainInfo> ServiceDomaininfos { get; set; }
        public virtual IDbSet<LanguagePair> LanguagePairs { get; set; }
        public virtual IDbSet<TranslationDomain> TranslationDomains { get; set; }

        public virtual IDbSet<Promotion> Promotions { get; set; }

        public virtual IDbSet<ProviderAvailabilityStatus> ProviderAvailabilityStatuses { get; set;}
        public virtual IDbSet<FreelanceCalendarPeriod> FreelanceCalendarPeriods { get; set; }

        public virtual IDbSet<Job> Jobs { get; set; }

        public virtual IDbSet<JobCompletionStatus> JobCompletionStatuses { get; set; }

        public virtual IDbSet<Currency> Currencies { get; set; }
        public virtual IDbSet<ServiceUOM> ServiceUOMs { get; set; }

        public virtual IDbSet<ProviderGroup> ProviderGroups { get; set; }

        public virtual IDbSet<JobPart> JobParts { get; set; }
        public virtual IDbSet<JobPartCompletionStatus> JobPartCompletionStatuses { get; set; }

        public virtual IDbSet<ProviderSoft> Software { get; set; }

        public virtual IDbSet<PhoneType> PhoneTypes { get; set; }

        public virtual IDbSet<OtherContact> OtherContacts { get; set; }
        public virtual IDbSet<OtherContactType> OtherContactTypes { get; set; }
        public virtual IDbSet<Phone> Phones { get; set; }

        public virtual IDbSet<Order> Orders { get; set; }
        public virtual IDbSet<Grade> Grades { get; set; }
        public virtual IDbSet<Customer> Customers { get; set; }

        public virtual IDbSet<JobUOM> JobUOMs { get; set; }
        public virtual IDbSet<JobType> JobTypes { get; set; }

        public virtual IDbSet<LegalForm> LegalForms { get; set; }

        public virtual IQueryable<T> Existing<T>() where T : class, ISoftDelete
        {
            return this.Set<T>().Where(x => !x.IsDeleted);
        }

        private async Task<int> GetPersonIdAsync(int aspNetUserId)
        {
            var result = await AspNetUsers
                .Where(x => x.Id == aspNetUserId)
                .Select(x => x.PersonId)
                .SingleAsync();
            
            return result;
        }

        public async Task<int> SaveChangesAsync(int aspNetUserId)
        {
            var personId = await GetPersonIdAsync(aspNetUserId);
            
            var entries = ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged);
            
            foreach (var entry in entries)
            {
                var timeStamped = entry.Entity as ITimeStamped;

                if (timeStamped != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        timeStamped.CreatedById = personId;
                        timeStamped.CreatedDate = DateTime.Now;
                    }

                    timeStamped.ModifiedById = personId;
                    timeStamped.ModifiedDate = DateTime.Now;
                }
            }

            foreach(var entry in entries.Where(x => x.State == EntityState.Deleted))
            {
                var deleted = entry.Entity as ISoftDelete;
                if (deleted != null)
                {
                    entry.State = EntityState.Modified;
                    deleted.IsDeleted = true;
                }
            }

            return await SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<Provider>()
                .HasMany(x => x.Groups)
                .WithMany(x => x.Providers);

            modelBuilder.Entity<Service>()
                .HasMany(x => x.Grades)
                .WithOptional(x => x.ServiceGraded)
                .WillCascadeOnDelete(false);
                

            modelBuilder.Configurations.Add(new OfficeMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new JobPartMapping());
            modelBuilder.Configurations.Add(new JobMapping());
        }
    }
}
