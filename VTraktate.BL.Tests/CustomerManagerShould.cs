using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTraktate.BL.Customers;
using VTraktate.BL.Orders;
using Moq;
using System.Linq;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using VTraktate.Domain.ComplexTypes;
using System.Linq.Expressions;

namespace VTraktate.BL.Tests
{
    [TestClass]
    public class CustomerManagerShould
    {
        public CustomerManagerShould()
        {
            var moqRepo = new Mock<ICustomerRepo>();
            var repo = moqRepo.Object;

            IQueryable<Person> fakePersons = new List<Person> 
            { 
                new Person 
                { 
                    Customers = new List<Customer> 
                    { new Customer 
                        { Id = 1 } 
                    }, 
                    PersonName = new IndividualName { FullName = "Чайковский "},
                    Phones = new List<Phone> 
                    {

                    },
                    Emails = new List<Email>
                    {

                    }
                }
            }.AsQueryable();

            moqRepo.Setup(x => x.GetAny(It.IsAny<Expression<Func<Person, bool>>>())).Returns(fakePersons);
            SUT = new CustomerManager(repo, new OrderNameMaker());
        }
        private CustomerManager SUT { get; set; }
        
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        
        

        private Order makeOrder(int i)
        {
            return new Order 
                { 
                    
                };
        }

        

        

        private IEnumerable<Person> CreateContactPersons(int count, int emailsCount, int phonesCount)
        {
            List<Person> result = new List<Person>();
            for(var i = 0; i < count; i++)
            {
                var person = new Person
                {
                    PersonName = new IndividualName { FullName = "ContactPerson # " + i.ToString() },
                    Emails = (ICollection<Email>)CreateEmailsList(emailsCount),
                    Phones = (ICollection<Phone>)CreatePhonesList(phonesCount)
                };
                result.Add(person);
            }
            return result;
        }

        private IEnumerable<Email> CreateEmailsList(int count)
        {
            return CreateList<Email>(count, i => new Email
            {
                Address = new EmailAddress
                {
                    Email = "test email # " + i.ToString()
                }
            });
        }

        private IEnumerable<Phone> CreatePhonesList(int count)
        {
            return CreateList<Phone>(count, i => new Phone { PhoneNumber = "test phone # " + i.ToString() });
        }

        private IEnumerable<T> CreateList<T>(int count, Func<int, T> factoryFunc) where T : class
        {
            List<T> result = new List<T> {};
            for (var i = 0; i < count; i++)
                result.Add(factoryFunc(i));
                return result;
        }



        
    }
}
