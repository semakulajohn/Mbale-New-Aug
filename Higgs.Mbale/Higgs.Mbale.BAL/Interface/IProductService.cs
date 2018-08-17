using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(long productId);
        long SaveProduct(Product product, string userId);
        void MarkAsDeleted(long productId, string userId);
    }
}
