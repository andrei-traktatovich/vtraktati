using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VTraktate.Repository;
using VTraktate.DataAccess;
using VTraktate.Domain;
using System.Linq;
using System.Collections.Generic;

namespace VTraktate.Repository.Tests
{
    [TestClass]
    public class WhenCalculatingMaxOrderNumberOrderRepositoryShould
    {
        [TestInitialize]
        public void Setup()
        {
            moqContext = new Mock<TraktatContext>();
            SUT = new OrderRepo(moqContext.Object);
            testOrders = MakeTestOrders();
        }

        Mock<TraktatContext> moqContext;
        public OrderRepo SUT { get; set; }

        [TestMethod]
        public void Return1IfNullOrders()
        {
            var customer = new Customer 
            {
                Id = 1,
                NumberPerOffice = false
            };

            ContextWillReturnOrders(ReturnNull);
            
            var result = SUT.GetNextNumber(customer, 0);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Return1IfEmptyOrders()
        {
            var customer = new Customer
            {
                Id = 1,
                NumberPerOffice = false
            };

            ContextWillReturnOrders(ReturnEmpty);

            var result = SUT.GetNextNumber(customer, 0);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ReturnMaxNumberForOrdersNumberedPerOffice()
        {
            var customer = new Customer
            {
                Id = 1,
                NumberPerOffice = true
            };

            var orders = testOrders.AsQueryable();

            moqContext.Setup(x => x.Existing<Order>()).Returns(orders);

            var result = SUT.GetNextNumber(customer, 1);

            Assert.AreEqual(MaxNumberForOrdersNumberedPerOffice, result);
        }

        [TestMethod]
        public void Return1AsMaxNumberForOrdersNumberedPerOfficeIfNoOrdersExistForThatOffice()
        {
            var customer = new Customer
            {
                Id = 666,
                NumberPerOffice = true
            };

            var orders = testOrders.AsQueryable();

            moqContext.Setup(x => x.Existing<Order>()).Returns(orders);

            var result = SUT.GetNextNumber(customer, 1);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ReturnMaxNumberForOrdersNumberedNonPerOfficeIf()
        {
            var customer = new Customer
            {
                Id = 1,
                NumberPerOffice = false
            };

            var orders = testOrders.AsQueryable();

            moqContext.Setup(x => x.Existing<Order>()).Returns(orders);

            var result = SUT.GetNextNumber(customer, 1);

            Assert.AreEqual(MaxNumberForOrdersNumberedNotPerOffice, result);
        }

        private const int MaxNumberForOrdersNumberedPerOffice = 10;
        private const int MaxNumberForOrdersNumberedNotPerOffice = 15; 
        private IQueryable<Order> ReturnTestOrders()
        {
            IQueryable<Order> result = testOrders as IQueryable<Order>;
            return result;
        }

        private List<Order> testOrders;
        
        private List<Order> MakeTestOrders() 
        {
            return new List<Order>
            {
                new Order { Number = 9, CustomerId = 1, OfficeId = 1 },
                new Order { Number = 14, CustomerId = 1, OfficeId = 0 },
                new Order { Number = 145, CustomerId = 12, OfficeId = 0 },
                new Order { Number = 148, CustomerId = 12, OfficeId = 1 }
            };
        }

        private IQueryable<Order> ReturnNull()
        {
            IQueryable<Order> result = null;
            return result;
        }

        private IQueryable<Order> ReturnEmpty()
        {
            IQueryable<Order> result = new List<Order>().AsQueryable();
            return result;
        }
        private void ContextWillReturnOrders(Func<IQueryable<Order>> func)
        {
            moqContext.Setup(x => x.Existing<Order>()).Returns(func);
        }

    }
}
