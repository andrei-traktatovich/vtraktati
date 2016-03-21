using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VTraktate.Core.Interfaces.BusinessLogic.Orders;
using System.Data.Entity;
using VTraktate.Models.Order;
using VTraktate.Domain;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Models.Order.ManagerOrderGrid;
using VTraktate.Domain.Snapshots;
using AutoMapper;
using AutoMapper.QueryableExtensions;


namespace VTraktate.Controllers
{
    public class OrderController : AuthenticatedControllerBase
    {
        public OrderController(IOrderManager orderManager)
        {
            this._orderManager = orderManager;
        }
        
        private IOrderManager _orderManager;

        [HttpGet]
        [Route("api/order/document")]
        public async Task<IHttpActionResult> GetDocument([FromUri] string text)
        {
            IEnumerable<string> result = await _orderManager.GetDocumentOptions(text).ToListAsync();
            return Ok(result);
        }

        

        [HttpPost]
        [Route("api/order/create")]
        public async Task<IHttpActionResult> CreateOrder([FromBody] OrderCreateBindingModel model)
        {
            // validate 
            var orderGraph = Mapper.Map<Order>(model);
            _orderManager.AssignNumber(orderGraph, orderGraph.Number, null);
            await _orderManager.SaveAsync(orderGraph, UserId);
            // temp !!! 
            return Ok(new { name = orderGraph.Name });
        }
    }
}