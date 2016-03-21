using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;
using VTraktate.Domain.Extensions;

namespace VTraktate.Domain.Tests.Extensions.ICompletableCalendarPeriodExtensions
{
    internal class TestCompletable : ICompletableCalendarPeriod
    {

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }

    [TestClass]
    public class IsOverDueShould
    {
        
        [TestMethod][Ignore]
        public void IsOverDueReturnsFalseIfNotCompleted()
        {
            var SUT = new TestCompletable { CompletionDate = null };
            Assert.IsFalse(SUT.IsOverDue());
        }

        [TestMethod]
        public void IsOverDueReturnsFalseIfCompletionDateEqualOrLessThanEndDate()
        {
            var dt = DateTime.Now;
            var SUT = new TestCompletable { StartDate = dt, EndDate = dt.AddMinutes(1), CompletionDate = dt.AddMinutes(1) };
            Assert.IsFalse(SUT.IsOverDue());
            SUT.CompletionDate = SUT.CompletionDate.Value.AddSeconds(-1);
            Assert.IsFalse(SUT.IsOverDue());
        }
    }

    [TestClass]
    public class IsPastDueShould
    {
        [TestMethod]
        public void IsPastDueReturnsFalseIsNotCompleted()
        {
            var sut = new TestCompletable { CompletionDate = null };
            Assert.IsFalse(sut.IsCompletedPastDue());
        }

        [TestMethod]
        public void IsPastDueReturnsFalseIsCompletionDateBeforeOrEqualEndDate()
        {
            var dt = DateTime.Now;
            var sut = new TestCompletable { StartDate = dt, EndDate = dt.AddMinutes(1), CompletionDate = dt.AddMinutes(1) };
            Assert.IsFalse(sut.IsCompletedPastDue());
            var sut2 = new TestCompletable { StartDate = dt, EndDate = dt.AddMinutes(1), CompletionDate = dt.AddSeconds(30) };
            Assert.IsFalse(sut2.IsCompletedPastDue());
        }

        [TestMethod]
        public void IsPastDueReturnsTrueIfCompletionDateAfterEndDate()
        {
            var dt = DateTime.Now;
            
            var sut = new TestCompletable { StartDate = dt, EndDate = dt.AddMinutes(1), CompletionDate = dt.AddSeconds(300) };
            Assert.IsTrue(sut.IsCompletedPastDue());
        }
    }
}
