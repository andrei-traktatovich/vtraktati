using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTraktate.Core.Interfaces;
using VTraktate.Domain;
using VTraktate.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace VTraktate.BL.Tests.CalendarService
{
    [TestClass]
    public class InsertShould
    {
        public class TestType : ISoftDelete, ICalendarPeriod
        {

            public bool IsDeleted { get; set; }

            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        private CalendarService<TestType> Service;
        private TestType DefaultItem;

        private List<TestType> Items;

        [TestInitialize]
        public void Setup()
        {
            // TODO: replace cloneFunc using reflection ??? 
            Func<TestType, TestType> CloneFunc = x => new TestType() { IsDeleted = x.IsDeleted, StartDate = x.StartDate, EndDate = x.EndDate };
            Service = new CalendarService<TestType>(CalendarService<TestType>.CalendarTimeScales.Day, CloneFunc);
            DefaultItem = new TestType { EndDate = new DateTime(2015, 05, 01), StartDate = new DateTime(2015, 04, 01), IsDeleted = false };
            Items = new List<TestType> { DefaultItem };
        }

        [TestMethod][Ignore]
        public void LeaveExistingItemsUnchangedIfNewItemDoesntOverlap()
        {
            var newitem = new TestType { EndDate = new DateTime(2015, 06, 06), StartDate = new DateTime(2015, 06, 01), IsDeleted = false };
            Service.Insert(Items, newitem);
            Assert.AreEqual(DefaultItem, Items.First());
            
            Assert.AreEqual(newitem, Items.Last());

            Assert.AreEqual(2, Items.Count());
        }
        [TestMethod][Ignore]
        public void ClipExistingItemEndIfEndAfterNewItemStart()
        {
            var newitem = new TestType { StartDate = new DateTime(2015, 05, 1), EndDate = new DateTime(2015, 06, 01), IsDeleted = false };
            Service.Insert(Items, newitem);
            Assert.AreEqual(newitem, Items.Last());
            Assert.AreEqual(2, Items.Count());
            Assert.AreEqual(new DateTime(2015, 05, 01).AddDays(-1), Items.First().EndDate);
            Assert.AreEqual(new DateTime(2015, 04, 01), Items.First().StartDate);
            Assert.AreEqual(false, Items.First().IsDeleted);
        }

        [TestMethod][Ignore]
        public void ClipExistingItemStartIfBeforeNewItemEnd()
        {
            var newitem = new TestType 
            { 
                EndDate = new DateTime(2015, 04, 1), 
                StartDate = new DateTime(2015, 03, 01), 
                IsDeleted = false 
            };
            
            Service.Insert(Items, newitem);
            Assert.AreEqual(newitem, Items.Last());
            Assert.AreEqual(2, Items.Count());
            Assert.AreEqual(new DateTime(2015, 04, 01).AddDays(1), Items.First().StartDate);
            Assert.AreEqual(new DateTime(2015, 05, 01), Items.First().EndDate);
            Assert.AreEqual(false, Items.First().IsDeleted);
        }

        [TestMethod][Ignore]
        public void MarkExistingItemDeletedIfContainedInNewItem()
        {
            var newitem = new TestType
            {
                StartDate = new DateTime(2015, 04, 1),
                EndDate = new DateTime(2015, 05, 02),
                IsDeleted = false
            };

            Service.Insert(Items, newitem);
            Assert.AreEqual(newitem, Items.Last());
            Assert.AreEqual(true, Items.First().IsDeleted);
        }
        
        [TestMethod][Ignore]
        public void SplitExistingItemIfItIsContainedInNewItem()
        {
            var newitem = new TestType
            {
                StartDate = new DateTime(2015, 04, 5),
                EndDate = new DateTime(2015, 04, 25),
                IsDeleted = false
            };
            Service.Insert(Items, newitem);
            Assert.AreEqual(3, Items.Count);
            var firstItem = Items.First();
            var secondItem = Items[1];

            Assert.AreEqual(newitem, Items.Last());

            Assert.AreEqual(new DateTime(2015, 04, 01), firstItem.StartDate);
            Assert.AreEqual(new DateTime(2015, 04, 04), firstItem.EndDate.Value);
            Assert.AreEqual(new DateTime(2015, 04, 26), secondItem.StartDate);
            Assert.AreEqual(new DateTime(2015, 05, 01), secondItem.EndDate);
            
        }
    }
}
