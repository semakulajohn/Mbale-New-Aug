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
    public class DeliveryApiController : ApiController
    {
            private IDeliveryService _deliveryService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(DeliveryApiController));
            private string userId = string.Empty;

            public DeliveryApiController()
            {
            }

            public DeliveryApiController(IDeliveryService deliveryService,IUserService userService)
            {
                this._deliveryService = deliveryService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetDelivery")]
            public Delivery GetDelivery(long deliveryId)
            {
                return _deliveryService.GetDelivery(deliveryId);
            }

            [HttpGet]
            [ActionName("GetAllDeliveries")]
            public IEnumerable<Delivery> GetAllDeliveries()
            {
                return _deliveryService.GetAllDeliveries();
            }

            [HttpGet]
            [ActionName("GetAllBranchDeliveries")]
            public IEnumerable<Delivery> GetAllBranchDeliveries(long branchId)
            {
                return _deliveryService.GetAllDeliveriesForAParticularBranch(branchId);
            }
            [HttpGet]
            [ActionName("GetAllDeliveriesForAParticularOrder")]
            public IEnumerable<Delivery> GetAllDeliveriesForAParticularOrder(long orderId)
            {
                return _deliveryService.GetAllDeliveriesForAParticularOrder(orderId);
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteDelivery(long deliveryId)
            {
                _deliveryService.MarkAsDeleted(deliveryId, userId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(Delivery model)
            {
                var deliveryId = _deliveryService.SaveDelivery(model, userId);
                return deliveryId;
            }
    }
}
