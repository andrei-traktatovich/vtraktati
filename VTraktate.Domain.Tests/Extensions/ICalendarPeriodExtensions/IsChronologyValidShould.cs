using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTraktate.Domain.Interfaces;
using VTraktate.Domain.Extensions;

namespace VTraktate.Domain.Tests.Extensions.ICalendarPeriodExtensions
{
    [TestClass]
    public class IsChronologyValidShould
    {
        internal class TestCalendarPeriod : ICalendarPeriod
        {

            public DateTime StartDate { get;set;}
            public DateTime? EndDate { get; set; }
        }
        [TestMethod]
        public void IsChronologyValidShouldReturnTrueIfEndDateIsNull()
        {
            ICalendarPeriod SUT = new TestCalendarPeriod { StartDate = DateTime.Now, EndDate = null };
            Assert.IsTrue(SUT.IsChronologyValid());
        }

        [TestMethod]
        public void IsChronologyValidShouldReturnTrueIfEndDateHasValueAndMoreThanStartDate()
        {
            ICalendarPeriod SUT = new TestCalendarPeriod { StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(1) };
            Assert.IsTrue(SUT.IsChronologyValid());
        }
        [TestMethod]
        public void IsChronologyValidShouldReturnFalseIfEndDateHasValueAndLessThanStartDate()
        {
            ICalendarPeriod SUT = new TestCalendarPeriod { StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(-1) };
            Assert.IsFalse(SUT.IsChronologyValid());
        }
        

    }
}
