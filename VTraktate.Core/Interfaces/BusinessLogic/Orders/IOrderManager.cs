using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Core.Interfaces.BusinessLogic.Orders
{
    public interface IOrderManager
    {
        IQueryable<string> GetDocumentOptions(string substring, int? maxOptions = null);

        Order AssignNumber(Order order, int? desiredNumber, string numberPostfix, bool shouldEnumerateOrder = true);

        Task SaveAsync(Order orderGraph, int UserId);

        IQueryable<Order> GetOrderGraphs();
    }

}
