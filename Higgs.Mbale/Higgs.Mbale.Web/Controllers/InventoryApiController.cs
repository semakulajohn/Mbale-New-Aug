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
    public class InventoryApiController : ApiController
    {
         private IInventoryService _inventoryService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(InventoryApiController));
            private string userId = string.Empty;

            public InventoryApiController()
            {
            }

            public InventoryApiController(IInventoryService inventoryService,IUserService userService)
            {
                this._inventoryService = inventoryService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetInventory")]
            public Inventory GetInventory(long inventoryId)
            {
                return _inventoryService.GetInventory(inventoryId);
            }

            [HttpGet]
            [ActionName("GetAllInventories")]
            public IEnumerable<Inventory> GetAllInventories()
            {
                return _inventoryService.GetAllInventories();
            }

            [HttpGet]
            [ActionName("GetAllInventoriesForParticularStore")]
            public IEnumerable<Inventory> GetAllInventoriesForAParticularStore(long storeId)
            {
                return _inventoryService.GetAllInventoriesForAParticularStore(storeId);
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteInventory(long inventoryId)
            {
                _inventoryService.MarkAsDeleted(inventoryId, userId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(Inventory model)
            {
                var InventoryId = _inventoryService.SaveInventory(model, userId);
                return InventoryId;
            }
    }
}
