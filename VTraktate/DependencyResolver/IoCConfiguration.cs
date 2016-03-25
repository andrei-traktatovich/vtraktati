using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.DataAccess;
using VTraktate.Domain;
using VTraktate.Repository;
using VTraktate.Filtering;
using VTraktate.Repository.SnapshotProviders;
using VTraktate.Core.Interfaces;
using VTraktate.Domain.Snapshots;
using VTraktate.Validation;
using VTraktate.Controllers;
using VTraktate.BL;
using VTraktate.BL.Cerberos;
using VTraktate.Core.Interfaces.BusinessLogic.Customers;
using VTraktate.Core.Interfaces.BusinessLogic.Orders;
using VTraktate.BL.Orders;
using VTraktate.BL.Customers;
using VTraktate.BL.Providers;
using VTraktate.Core.Interfaces.BusinessLogic.Orders.JobParts;
using VTraktate.Core.Interfaces.BusinessLogic.Orders.Netting;
using VTraktate.Core.Interfaces.BusinessLogic.Providers;

namespace VTraktate.DependencyResolver
{
    public class IoCConfiguration
    {
        public static void ConfigureAll(ref UnityContainer container)
        {
            container.RegisterType<TraktatContext, TraktatContext>(new HierarchicalLifetimeManager());


            container.RegisterType<IAccountRepo, AccountRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IRepo<Provider>, ProviderRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IServiceLanguageInfoRepo, ServiceLanguageRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IServiceDomainRepo, ServiceDomainInfoRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IServiceRepo, ProviderServiceRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IRepo<Person>, VTraktate.Repository.PersonRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IRepo<Email>, VTraktate.Repository.EmailRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IRepo<Phone>, VTraktate.Repository.PhoneRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IRepo<Grade>, VTraktate.Repository.GradeRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IRepo<OtherContact>, VTraktate.Repository.OtherContactRepo>(new PerResolveLifetimeManager());
            container.RegisterType<ICustomerRepo, VTraktate.Repository.CustomerRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IOrderRepo, VTraktate.Repository.OrderRepo>(new PerResolveLifetimeManager());
            container.RegisterType<GradeRepo, GradeRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IRepo<ProviderSoft>, VTraktate.Repository.ProviderSoftRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IJobRepo, JobRepo>(new PerResolveLifetimeManager());
            container.RegisterType<IRepo<JobPart>, JobPartRepo>(new PerResolveLifetimeManager());
            
            container.RegisterType<EmploymentRepo, EmploymentRepo>(new PerResolveLifetimeManager());
            container.RegisterType<ISnapshotProvider<AccountSnapshot>, AccountSnapshotProvider>();
            container.RegisterType<ISnapshotProvider<ProviderSnapshot>, ProviderSnapshotProvider>();
            container.RegisterType<IGlobalsProvider, GlobalsProvider>();
            container.RegisterType<ISnapshotProvider<ExtendedProviderSnapshot>, ExtendedProviderSnapShotProvider>();

            container.RegisterInstance<IQueryFilterService<ExtendedProviderSnapshot>>(new QueryFilterService<ExtendedProviderSnapshot>(new ExtendedProviderSnapshotFilteringRules()));
            container.RegisterInstance<IQueryFilterService<AccountSnapshot>>(new QueryFilterService<AccountSnapshot>(new AccountSnapshotFilteringRules()));
            container.RegisterInstance<IQueryFilterService<Job>>(new QueryFilterService<Job>(new JobFilteringRules()));
            container.RegisterInstance<IQueryFilterService<Grade>>(new GradeFilterService(new GradeFilteringRules()));

            container.RegisterType<Validator, Validator>();

            container.RegisterType<DomainsManager, DomainsManager>();
            container.RegisterType<ProviderServiceManager, ProviderServiceManager>();
            container.RegisterType<ICustomerManager, CustomerManager>();
            container.RegisterType<IOrderNameMaker, OrderNameMaker>();
            container.RegisterType<IOrderManager, OrderManager>();
            container.RegisterType<IJobManager, JobManager>();
            container.RegisterType<IJobPartManager, JobPartManager>();

            container.RegisterType<INettingManager, NettingManager>();

            container.RegisterInstance<ICalendarService<Freelance>>(new FreelanceCalendarService());
            container.RegisterInstance<ICalendarService<Employment>>(new EmploymentCalendarService());
            container.RegisterInstance<ICalendarService<FreelanceCalendarPeriod>>(new AvailabilityCalendarService());

            container.RegisterType<ICerberosMum, CerberosMum>();

            container.RegisterType<ITraktatContext, TraktatContext>();

            container.RegisterType<IProviderManagerFactory, ProviderManagerFactory>();

        }
    }
}