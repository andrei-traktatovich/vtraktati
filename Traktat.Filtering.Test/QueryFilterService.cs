using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTraktate.Filtering;
using Microsoft.CSharp;

namespace Traktat.Filtering.Test
{
    using Expr = System.Linq.Expressions.Expression<Func<TestSequence,bool>>;
    using System.Collections.Generic;
    using System.Linq;
    using VTraktate.Core.Interfaces.Filtering;
    using System.Linq.Expressions;

    #region infrastructure 

    public class TestSequence
    {
        public TestSequence()
        {
            ProfessionDescription = new ProfessionDescription();
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public ProfessionDescription ProfessionDescription { get; set; }
    }

    public class TestFilter : IFilterModel
    {
        public string Name { get; set; }
        public int? Age { get; set; }

        public ProfessionDescription ProfessionDescription { get; set; }
    }

    public class ProfessionDescription
    {
        public string Name { get; set; }
    }
    
    public class EmptyTestFilterRules : IFilteringRules
    {

        public string DefaultSort
        {
            get { return "Name"; }
        }
    }
    public class TestFilterRules : IFilteringRules
    {
        public Expr Name(object value)
        {
            var val = value.ToString();
            return x => x.Name == val;
        }

        public Expr Age(object value)
        {
            var val = Int32.Parse(value.ToString());
            return x => x.Age == val;
        }

        public Expr ProfessionDescription(object value)
        {
            var val = (ProfessionDescription)value;
            return x => x.ProfessionDescription.Name == val.Name;
        }

        public string DefaultSort
        {
            get { return "Name asc"; }
        }
    }


    public class TestSortModel : ISortModel
    {
        public string Name { get; set; }
    }
    public class TestTransform
    {

    }
    #endregion

    [TestClass]
    public class QueryFilterService
    {
        IQueryable<TestSequence> _sequence;
        private TestFilterRules _filter;
        private TestFilter _filterModel;
        private TestFilterRules _filteringRules;
        private QueryFilterService<TestSequence> _sut;
        private TestSortModel _sortModel;

        [TestInitialize]
        public void Setup()
        {
            _sequence = new List<TestSequence> 
            { 
                new TestSequence { Age = 3, Name = "O" },
                new TestSequence { Age = 5, Name = "O" },
                new TestSequence { Age = 3, Name = "A", ProfessionDescription = new ProfessionDescription { Name = "plumber" } }
            }.AsQueryable();

            _filter = new TestFilterRules();
            _filterModel = new TestFilter();
            _filteringRules = new TestFilterRules();
            _sortModel = new TestSortModel(); 
            _sut = new QueryFilterService<TestSequence>(_filteringRules);
        }

        #region testFiltering
        [TestMethod]
        public void FilterShouldReturnSourceIfNoFilterFunctionsSpecifiedInFilteringRules()
        {
            _filterModel.Age = 3;
            var emptySut = new QueryFilterService<TestSequence>(new EmptyTestFilterRules());
            var result = emptySut.Filter(_sequence, _filterModel, true);

            Assert.AreEqual(_sequence.Count(), result.Count());
        }
        
        [TestMethod]
        public void FilterShouldReturnSourceIfFilterModelIsEmpty()
        {
            var result = _sut.Filter(_sequence, _filterModel, true);

            Assert.AreEqual(_sequence.Count(), result.Count());
        }

        [TestMethod]
        public void FilterShouldReturnSourceIfNullFilterModel()
        {
            var result = _sut.Filter(_sequence, null, true);
            Assert.AreEqual(_sequence.Count(), result.Count());
        }

        [TestMethod]
        public void ReturnOnlyItemsPassingAllConditions()
        {
            _filterModel.Age = 3;
            var result = _sut.Filter(_sequence, _filterModel, true);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void ReturnZeroCountIfNoItemPassesConditions()
        {
            _filterModel.Age = 33;
            _filterModel.Name = "O";
            
            var result = _sut.Filter(_sequence, _filterModel, true);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void WorkWithComplexObjects()
        {
            _filterModel.ProfessionDescription = new ProfessionDescription { Name = "plumber" };
            
            var result = _sut.Filter(_sequence, _filterModel, true);

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void WorkWithMultipleConditions()
        {
            _filterModel.ProfessionDescription = new ProfessionDescription { Name = "plumber" };
            _filterModel.Name = "A";
            var result = _sut.Filter(_sequence, _filterModel, true);

            Assert.AreEqual(1, result.Count());
        }
        #endregion

        #region test ordering
        [TestMethod]
        public void FallBackOnDefaultOrderingIfNoneSpecified()
        {
            var result = _sut.Sort(_sequence, _sortModel);
            var sorted = _sequence.OrderBy(x => x.Name);
            
            var test = sorted.Zip<TestSequence, TestSequence, bool>(result, (x, y) => x.Name == y.Name);

            Assert.IsTrue(test.All(x => x == true));
        }

        [TestMethod]
        public async void ReturnPagedResult()
        {
            var sorted = _sequence.OrderBy(x => x.Name).ToList();
            for (int i = 0; i < sorted.Count(); i++)
            {
                var page = await _sut.GetFilteredOrderedPageAsync<TestSequence>(_sequence, _filterModel, _sortModel, i + 1, 1, x => x);
                Assert.IsNotNull(page);
                Assert.AreEqual(page.Total, _sequence.Count());
                var expected = sorted.Skip(i).Take(1).First();
                var actual = page.Result.First();
                Assert.AreEqual(expected, actual);
            }
        }
        
        
        #endregion
    }
     

}
