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
    public class ProductApiController : ApiController
    {
            private IProductService _productService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(ProductApiController));
            private string userId = string.Empty;

            public ProductApiController()
            {
            }

            public ProductApiController(IProductService productService,IUserService userService)
            {
                this._productService = productService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetProduct")]
            public Product GetProduct(long productId)
            {
                return _productService.GetProduct(productId);
            }

            [HttpGet]
            [ActionName("GetAllProducts")]
            public IEnumerable<Product> GetAllProducts()
            {
                return _productService.GetAllProducts();
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteProduct(long ProductId)
            {
                _productService.MarkAsDeleted(ProductId, userId);
            }


            [HttpPost]
            [ActionName("Save")]
            public long Save(Product model)
            {

                var ProductId = _productService.SaveProduct(model, userId);
                return ProductId;
            }
    }
}
