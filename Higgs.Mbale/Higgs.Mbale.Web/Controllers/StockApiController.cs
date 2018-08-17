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
    public class StockApiController : ApiController
    {
            private IStockService _stockService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(StockApiController));
            private string userId = string.Empty;

            public StockApiController()
            {
            }

            public StockApiController(IStockService stockService,IUserService userService)
            {
                this._stockService = stockService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetStock")]
            public Stock GetStock(long stockId)
            {
                return _stockService.GetStock(stockId);
            }

            [HttpGet]
            [ActionName("GetAllStocks")]
            public IEnumerable<Stock> GetAllStocks()
            {
                return _stockService.GetAllStocks();
            }

            [HttpGet]
            [ActionName("GetAllStocksForAparticularBranch")]
            public IEnumerable<Stock> GetAllStocksForAparticularBranch(long branchId)
            {
                return _stockService.GetAllStocksForAParticularBranch(branchId);
            }

            [HttpGet]
            [ActionName("GetAllStocksForAparticularStore")]
            public IEnumerable<StoreStock> GetAllStocksForAparticularStore(long storeId)
            {
                return _stockService.GetStocksForAParticularStore(storeId);
            }
            [HttpGet]
            [ActionName("GetStoreFlourStock")]
            public StoreGrade GetStoreFlourStock(long storeId)
            {
                return _stockService.GetStoreFlourStock(storeId);
            }
          

         
    }
}
