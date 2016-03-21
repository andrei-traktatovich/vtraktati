using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;

namespace VTraktate.Core.Interfaces.BusinessLogic.Customers
{
    public interface ICustomerManager
    {
        ICustomerRepo Repo { get; }

        IQueryable<Customer> FindCustomersByName(string substring);

        Task<Customer> FindCustomerByIdAsync(int customerId);
        Task<CustomerProfile> GetCustomerProfileAsync(int customerId, int officeId);
        IQueryable<Person> MatchContactsByNameOrContactInfo(int customerId, string match);

    }

    
    
}