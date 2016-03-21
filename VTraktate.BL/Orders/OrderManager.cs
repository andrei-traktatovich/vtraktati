using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.BusinessLogic.Orders;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using VTraktate.Domain.Snapshots;
using System.Data.Entity;

namespace VTraktate.BL.Orders
{
    public class OrderManager : IOrderManager
    {
        public OrderManager(IOrderRepo repository, IOrderNameMaker nameMaker)
        {
            this.Repository = repository;
            this.OrderNameMaker = nameMaker;
        }

        private const int MAX_DOCUMENT_OPTIONS = 100; // not more than 100 options !!!

        public IOrderRepo Repository { get; private set; }
        public IOrderNameMaker OrderNameMaker { get; private set; }
        public IQueryable<string> GetDocumentOptions(string substring, int? maxOptions = null)
        {
            // this one is incorrect because it is referring to orders rather than jobs
            var result = Repository
                .GetAny<Job>(x => x.Document != null && x.Document.Contains(substring))
                .Select(x => x.Document)
                .Distinct()
                .OrderBy(x => x);
            
            if(maxOptions.GetValueOrDefault() == -1)
                return result;
            else 
                return result.Take(maxOptions ?? MAX_DOCUMENT_OPTIONS);
        }

        public Order AssignNumber(Order order, int? desiredNumber, string numberPostfix, bool shouldEnumerateOrder = true)
        {
            if (order == null)
                throw new ArgumentNullException("order");
            var customerId = order.CustomerId;
            var officeId = order.OfficeId;

            OrderNumberComponents orderNumberInfo = Repository.GetOrderNumberInfo(customerId, officeId, shouldEnumerateOrder);

            if (desiredNumber.HasValue && shouldEnumerateOrder)
                orderNumberInfo.Number = desiredNumber.Value;

            order.Number = (desiredNumber.HasValue && shouldEnumerateOrder) ? desiredNumber.Value : orderNumberInfo.Number;
            order.Name = OrderNameMaker.MakeNumber(orderNumberInfo);

            return order;
        }



        public async Task SaveAsync(Order orderGraph, int UserId)
        {
            Repository.Create(orderGraph);
            await Repository.SaveAsUserAsync(UserId);
        }


        public IQueryable<Order> GetOrderGraphs()
        {
            // this is a temp solution only 
            return Repository.GetGraphs().OrderByDescending(x => x.CreatedDate);
        }
    }
}
