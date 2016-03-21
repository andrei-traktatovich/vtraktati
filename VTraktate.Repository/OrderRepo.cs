using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using VTraktate.DataAccess;
using System.Data.Entity;
using VTraktate.Domain.ComplexTypes;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Repository
{
    public class OrderRepo : Repo<Order>, IOrderRepo
    {
        public OrderRepo(TraktatContext ctx) : base(ctx)
        {

        }
        public override IQueryable<Order> GetGraphs(System.Linq.Expressions.Expression<Func<Order, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy)
                .Include(x => x.Jobs)
                .Include(x => x.Jobs.Select(y => y.JobType))
                .Include(x => x.Jobs.Select(y => y.Language))
                .Include(x => x.Jobs.Select(y => y.Domain1))
                .Include(x => x.Jobs.Select(y => y.Domain2))
                .Include(x => x.Jobs.Select(y => y.CreatedBy))
                .Include(x => x.Jobs.Select(y => y.ModifiedBy));
        }

        public int GetNextNumber(int customerId, int officeId)
        {
            var customer = Context.Customers.Find(customerId);

            if (customer == null)
                throw new InvalidOperationException(string.Format("Заказчик с ID {0} не найден.", customerId));

            return GetNextNumber(customer, officeId);
        }

        public int GetNextNumber(Customer customer, int officeId)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var query = Context.Existing<Order>();
            if (query == null)
                return 1;

            query = query.Where(x => x.CustomerId == customer.Id);

            if (customer.NumberPerOffice)
                query = query.Where(x => x.OfficeId == officeId);
            // todo: encapsualte this somewhere ...
            var max = query.Max(x => (int?)x.Number) ?? 0;
            return max + 1;
        }


        public OrderNumberComponents GetOrderNumberInfo(int customerId, int officeId, bool shouldEnumerateOrder)
        {
            var customer = GetAny<Customer>(x => x.Id == customerId).SingleOrDefault();
            if (customer == null)
                throw new InvalidOperationException(string.Format("Customer ID {0} no found", customerId));

            string customerCode = customer.Code;

            var office = GetAny<Office>(x => x.Id == officeId).SingleOrDefault();
            if (office == null)
                throw new InvalidOperationException(string.Format("Office ID {0} no found", officeId));

            int number = GetNextNumber(customer, officeId);
            string officeCode = office.Code;
            bool useOfficeCode = customer.UseOfficeCode;

            var result = new OrderNumberComponents
            {
                CustomerCode = customerCode,
                Number = number,
                OfficeCode = officeCode,
                PostFix = "",
                useOfficeCode = useOfficeCode

            };
            return result;
        }


        public void Create(Order orderGraph)
        {
            if (!IsNameUnique(orderGraph.Name))
                throw new InvalidOperationException(string.Format("Создание заказа невозможно: имя заказа {0} не является уникальным. Измените имя (номер заказа) и попытайтесь сохранить его еще раз.",
                    orderGraph.Name));

            AddOrUpdate(orderGraph);

            foreach (var job in orderGraph.Jobs)
            {
                // I need this for complex type to prevent Complex Type from being null.
                if (job.Final == null)
                    job.Final = new JobVolumeAndPricing { Pricing = new JobPricing(), Volume = new Volume() };
                Context.Entry(job).State = System.Data.Entity.EntityState.Added;
            }
        }

        private bool IsNameUnique(string p)
        {
            return (Context.Existing<Order>().All(x => x.Name != p));
        }
    }
}
