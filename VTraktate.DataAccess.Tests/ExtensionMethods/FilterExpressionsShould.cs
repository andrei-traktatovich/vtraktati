using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTraktate.Domain.Interfaces;
using System.Collections.Generic;
using VTraktate.DataAccess.ExtensionMethods;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;

namespace VTraktate.DataAccess.Tests.ExtensionMethods
{
    [TestClass]
    public class FilterExpressionsShould
    {
        public IQueryable<TestCalendarPeriod> Periods =
            new List<TestCalendarPeriod>
            {
                //new TestCalendarPeriod { StartDate = null, EndDate = null, IsCurrent = true },
                //new TestCalendarPeriod { StartDate = null, EndDate = DateTime.Now.AddDays(-1), IsCurrent = false },
                //new TestCalendarPeriod { StartDate = null, EndDate = DateTime.Now.AddDays(1), IsCurrent = true },
                new TestCalendarPeriod { StartDate = DateTime.Now, EndDate = null, IsCurrent = true },
                new TestCalendarPeriod { StartDate = DateTime.Now.AddDays(1), EndDate = null, IsCurrent = false },
                //new TestCalendarPeriod { StartDate = null, EndDate = null, IsCurrent = true, IsDeleted = true },
                //new TestCalendarPeriod { StartDate = null, EndDate = DateTime.Now.AddDays(-1), IsCurrent = false, IsDeleted = true },
                //new TestCalendarPeriod { StartDate = null, EndDate = DateTime.Now.AddDays(1), IsCurrent = true, IsDeleted = true },
                new TestCalendarPeriod { StartDate = DateTime.Now, EndDate = null, IsCurrent = true, IsDeleted = true },
                new TestCalendarPeriod { StartDate = DateTime.Now.AddDays(1), EndDate = null, IsCurrent = false, IsDeleted = true },
            }.AsQueryable();
        
        [TestMethod]
        public void CurrentShouldFilterOutItemsThatAreNotCurrent()
        {
            var result = Periods.Current();

            Assert.IsTrue(result.All(x => x.IsCurrent));
        }
        [TestMethod]
        public void ExistingShouldFilterOutItemsThatAreDeleted()
        {
            var result = Periods.Existing();
            Assert.IsTrue(result.All(x => !x.IsDeleted));
        }

        [TestMethod]
        public void CurrentExistingSHouldReturnExistingNonDeletedItems()
        {
            var result = Periods.ExistingCurrent();
            Assert.IsTrue(result.All(x => x.IsCurrent));
            Assert.IsTrue(result.All(x => !x.IsDeleted));
        }

        [TestMethod]
        public void CurrentAndExistingSHouldReturnTheSameType()
        {
            var t = typeof(TestCalendarPeriod);
            var result = Periods.ExistingCurrent();
            
            Assert.AreNotEqual(0, result.Count());
            Assert.IsInstanceOfType(result, typeof(IQueryable<TestCalendarPeriod>));
        }
    }

    public class TestCalendarPeriod : ICalendarPeriod, ISoftDelete
    {
        public TestCalendarPeriod ()
	    {
            Id++;
	    }

        public bool IsDeleted { get; set; }
        public bool IsCurrent { get; set; }
        public static int Id { get; private set;}

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
