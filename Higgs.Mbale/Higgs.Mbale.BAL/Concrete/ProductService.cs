using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;
using log4net;


namespace Higgs.Mbale.BAL.Concrete
{
    public class ProductService: IProductService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(ProductService));
        private IProductDataService _dataService;
        private IUserService _userService;
        

        public ProductService(IProductDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProduct(long productId)
        {
            var result = this._dataService.GetProduct(productId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProducts()
        {
            var results = this._dataService.GetAllProducts();
            return MapEFToModel(results);
        } 

       
        public long SaveProduct(Product product, string userId)
        {
            var productDTO = new DTO.ProductDTO()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Deleted = product.Deleted,
                CreatedBy = product.CreatedBy,
                CreatedOn = product.CreatedOn,

            };

           var ProductId = this._dataService.SaveProduct(productDTO, userId);

           return ProductId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long productId, string userId)
        {
            _dataService.MarkAsDeleted(productId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Product> MapEFToModel(IEnumerable<EF.Models.Product> data)
        {
            var list = new List<Product>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Product EF object to Product Model Object and
        /// returns the Product model object.
        /// </summary>
        /// <param name="result">EF Product object to be mapped.</param>
        /// <returns>Product Model Object.</returns>
        public Product MapEFToModel(EF.Models.Product data)
        {
          
            var product = new Product()
            {
                ProductId = data.ProductId,
                Name = data.Name,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),

            };
            return product;
        }



       #endregion
    }
}
