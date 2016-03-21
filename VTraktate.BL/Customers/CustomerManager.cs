using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.BusinessLogic.Customers;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using System.Data.Entity;
using VTraktate.Core.Interfaces.BusinessLogic.Orders;
using System.Linq.Expressions;

namespace VTraktate.BL.Customers
{
    public class CustomerManager : ICustomerManager
    {
        public CustomerManager(ICustomerRepo repo, IOrderNameMaker orderNameMaker)
        {
            Repo = repo;
            OrderNameMaker = orderNameMaker;
        }

        public ICustomerRepo Repo { get; private set; }
        public IOrderNameMaker OrderNameMaker { get; private set; }
        public IQueryable<Customer> FindCustomersByName(string substring)
        {
            return Repo.Get(x => x.LongName.Contains(substring) || x.ShortName.Contains(substring) || x.Code.Contains(substring));
        }

        public async Task<Customer> FindCustomerByIdAsync(int customerId)
        {
            return await Repo.FindByIdAsync(customerId);
        }

        public IQueryable<Person> GetContacts(int customerId)
        {
            return Repo.GetAny<Person>(x => x.Customers.Any(y => y.Id == customerId));
        }

        public IQueryable<Person> MatchContacts(int customerId, Expression<Func<Person, bool>> predicate = null)
        {
            //ATTN: this doesn't accomodate the fact that persons are ISoftDelete

            var data = GetContacts(customerId);//.Where(x => !x.IsDeleted);
            
            if (predicate != null)
                data = data.Where(predicate);

            return data;
        }

        
        public IQueryable<Person> MatchContactsByNameOrContactInfo(int customerId, string match)
        {
            //ATTN: this doesn't accomodate the fact that phonesm emails, otherContacts are ISoftDelete !!! 
            Expression<Func<Person, bool>> ContactMatchFunc = x => x.PersonName.FullName.Contains(match)
                || x.Phones.Any(phone => phone.PhoneNumber.Contains(match))
                || x.Emails.Any(email => email.Address.Email.Contains(match))
                || x.OtherContacts.Any(oContact => oContact.Address.Contains(match));

            return MatchContacts(customerId, ContactMatchFunc);
        }

        private async Task<Office> FindOfficeByIdAsync(int officeId)
        {
            return await Repo.FindAnyByIdAsync<Office>(officeId);
        }
        public Person GetDefaultContact(int customerId)
        {
            return GetContacts(customerId).FirstOrDefault(x => x.IsDefaultContactPerson);
        }
        public async Task<CustomerProfile> GetCustomerProfileAsync(int customerId, int officeId)
        {
            var customer = await FindCustomerByIdAsync(customerId);

            if (customer == null)
                throw new InvalidOperationException(string.Format("Заказчик с Id {0} не найден", customerId));

            var office = await FindOfficeByIdAsync(officeId);

            if (office == null)
                throw new InvalidOperationException(string.Format("Офис с id {0} не найден", officeId));

            var orderLiteral = OrderNameMaker.MakeLiteral(customer, office);

            var defaultContact = GetDefaultContact(customerId);

            var result = new CustomerProfile
            {
                IsIndividual = customer.IsIndividual,
                OrderLiteral = orderLiteral,
                RoundingPolicyId = customer.RoundingPolicyId,
                DefaultContactPerson = defaultContact
            };

            return result;
        }

    }
}
