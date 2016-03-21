using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTraktate.BL.Orders;
using VTraktate.Domain;

namespace VTraktate.BL.Tests.Orders
{
    [TestClass]
    public class OrderNameMakerShould
    {
        [TestInitialize]
        public void Setup()
        {
            SUT = new OrderNameMaker();
        }

        public OrderNameMaker SUT { get; set; }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RequireNonEmptyCustomerCode()
        {
            SUT.MakeLiteral("", "S", true);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RequireNonEmptyOfficeCodeIfOfficeCodeIsRequired()
        {
            SUT.MakeLiteral("PBL", null, true);
        }

        [TestMethod]
        public void MakeOrderLiteralConsistingOfOfficeNameUnderScoreAndCustomerNameIfOfficePrefixIsRequired()
        {
            var name = SUT.MakeLiteral("PBL", "S", true);
            Assert.AreEqual("S_PBL", name);
        }

        [TestMethod]
        public void AppendNumberToOrderLiteralWithoutPostfix()
        {
            var name = SUT.MakeNumber("PBL", "S", true, 345);
            Assert.AreEqual("S_PBL-345", name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RequirePostfixIfNoNumberSpecified()
        {
            var name = SUT.MakeNumber("PBL", "S", true, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RequireNumberIfNoPostfixSpecified()
        {
            var name = SUT.MakeNumber("PBL", "S", true, null, null);
        }

        [TestMethod]
        public void MakeNonNumberedName()
        {
            var name = SUT.MakeNumber("PBL", "S", true, null, "ОТ_ИСАЕВА");
            Assert.AreEqual("S_PBL-ОТ_ИСАЕВА", name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RequireOrderNumberToBeAPositiveIntegerGreaterThanZero()
        {
            var name = SUT.MakeNumber("PBL", "S", true, 0);
        }

        [TestMethod]
        public void MakeOrderNameWithNumberAndPostfix()
        {
            var name = SUT.MakeNumber("PBL", "S", true, 345, "ОТ_ИСАЕВА");
            Assert.AreEqual("S_PBL-345_ОТ_ИСАЕВА", name);
        }

        [TestMethod]
        public void MakeOrderLiteralFromCustomer()
        {
            var customer = new Customer()
            {
                Code = "BPL",
                UseOfficeCode = true
            };

            var office = new Office()
            {
                Code = "S"
            };
            string name = SUT.MakeLiteral(customer, office);
            Assert.AreEqual("S_BPL", name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenMakingOrderLiteralFromCustomerThrowIfCustomerOrOfficeAreNull()
        {
            SUT.MakeLiteral(null, null);
        }
    }
}
