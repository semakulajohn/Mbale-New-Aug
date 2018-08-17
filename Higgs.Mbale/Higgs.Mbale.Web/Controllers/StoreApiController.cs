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
    public class StoreApiController : ApiController
    {
         private IStoreService _storeService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(StoreApiController));
            private string userId = string.Empty;

            public StoreApiController()
            {
            }

            public StoreApiController(IStoreService storeService,IUserService userService)
            {
                this._storeService = storeService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetStore")]
            public Store GetStore(long storeId)
            {
                return _storeService.GetStore(storeId);
            }

            [HttpGet]
            [ActionName("GetAllStores")]
            public IEnumerable<Store> GetAllStores()
            {
                return _storeService.GetAllStores();
            }

            [HttpGet]
            [ActionName("GetAllBranchStores")]
            public IEnumerable<Store> GetAllBranchStores(long branchId)
            {
                return _storeService.GetAllStoresForAParticularBranch(branchId);
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteStore(long storeId)
            {
                _storeService.MarkAsDeleted(storeId, userId);
            }

         

            [HttpPost]
            [ActionName("Save")]
            public long Save(Store model)
            {

                var storeId = _storeService.SaveStore(model, userId);
                return storeId;
            }
    }
}
