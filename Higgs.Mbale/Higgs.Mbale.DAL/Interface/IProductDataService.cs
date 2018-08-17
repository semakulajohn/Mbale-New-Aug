using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;


namespace Higgs.Mbale.DAL.Interface
{
    public interface IProductDataService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(long productId);
        long SaveProduct(ProductDTO product, string userId);
        void MarkAsDeleted(long productId, string userId);
    }
}
