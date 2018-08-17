using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.DAL.Concrete;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.EF.UnitOfWork;
using Higgs.Mbale.DTO;
using log4net;


namespace Higgs.Mbale.DAL.Concrete
{
    public class ProductDataService : DataServiceBase,IProductDataService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(ProductDataService));

       public ProductDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
              
        public IEnumerable<Product> GetAllProducts()
        {
            return this.UnitOfWork.Get<Product>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public Product GetProduct(long ProductId)
        {
            return this.UnitOfWork.Get<Product>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.ProductId == ProductId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new Product or updates an already existing Product.
        /// </summary>
        /// <param name="Product">Product to be saved or updated.</param>
        /// <param name="ProductId">ProductId of the Product creating or updating</param>
        /// <returns>productId</returns>
        public long SaveProduct(ProductDTO productDTO, string userId)
        {
            long productId = 0;
            
            if (productDTO.ProductId == 0)
            {
           
                var product = new Product()
                {
                    Name = productDTO.Name,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false, 
                };

                this.UnitOfWork.Get<Product>().AddNew(product);
                this.UnitOfWork.SaveChanges();
                productId = product.ProductId;
                return productId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Product>().AsQueryable()
                    .FirstOrDefault(e => e.ProductId == productDTO.ProductId);
                if (result != null)
                {
                    result.Name = productDTO.Name;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = productDTO.Deleted;
                    result.DeletedBy = productDTO.DeletedBy;
                    result.DeletedOn = productDTO.DeletedOn;

                    this.UnitOfWork.Get<Product>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return productDTO.ProductId;
            }            
        }

        public void MarkAsDeleted(long ProductId, string userId)
        {

            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_Estate_And_RelatedData_AsDeleted(ProductId, userId);
            }

        }
    
    }
}
