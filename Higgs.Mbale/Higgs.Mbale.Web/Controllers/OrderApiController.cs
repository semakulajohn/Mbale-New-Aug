using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Higgs.Mbale.BAL.Interface;
using log4net;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.Web.Controllers
{
    public class OrderApiController : ApiController
    {
            private IOrderService _orderService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(OrderApiController));
            private string userId = string.Empty;

            public OrderApiController()
            {
            }

            public OrderApiController(IOrderService orderService,IUserService userService)
            {
                this._orderService = orderService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetOrder")]
            public Order GetOrder(long orderId)
            {
                return _orderService.GetOrder(orderId);
            }

            [HttpGet]
            [ActionName("GetAllOrders")]
            public IEnumerable<Order> GetAllOrders()
            {
                return _orderService.GetAllOrders();
            }

            [HttpGet]
            [ActionName("GetAllOrdersForAparticularBranch")]
            public IEnumerable<Order> GetAllOrdersForAparticularBranch(long branchId)
            {
                return _orderService.GetAllOrdersForAParticularBranch(branchId);
            }

            [HttpGet]
            [ActionName("GetAllOrdersForAParticularCustomer")]
            public IEnumerable<Order> GetAllOrdersForAParticularCustomer(string customerId)
            {
                return _orderService.GetAllOrdersForAParticularCustomer(customerId);
            }
            [HttpGet]
            [ActionName("Delete")]
            public void DeleteOrder(long orderId)
            {
                _orderService.MarkAsDeleted(orderId, userId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(Order model)
            {
                var orderId = _orderService.SaveOrder(model, userId);
                return orderId;
            }
    }
}
