using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Core.Repository.Interfaces
{
    public interface IOrderRepo : IRepo<Order>
    {
        int GetNextNumber(int customerId, int officeId);
        int GetNextNumber(Customer customer, int officeId);

        OrderNumberComponents GetOrderNumberInfo(int customerId, int officeId, bool shouldEnumerateOrder);

        void Create(Order orderGraph);
    }
}