using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VTraktate.BL.Orders;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;

namespace VTraktate.BL.Tests.Orders
{
    [TestClass]
    public class OrderManagerShould
    {
        [TestInitialize]
        public void Setup()
        {
            IOrderRepo moqRepo = MockOrderRepo();
            SUT = new OrderManager(moqRepo, new OrderNameMaker());
        }

        private IOrderRepo MockOrderRepo()
        {
            var moqRepo = new Mock<IOrderRepo>();
                
            return moqRepo.Object;
        }
        
        [TestMethod]
        public void AssignNumber()
        {
            var order = new Order 
            {
                Number = null,  // if number is null, then assign number automatically 
                OfficeId = 1,
                CustomerId = 1
            };
        }

        public OrderManager SUT { get; set; }
    }
}
