using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IStoreService
    {
        IEnumerable<Store> GetAllStores();
        Store GetStore(long storeId);
        long SaveStore(Store store, string userId);
        void MarkAsDeleted(long storeId, string userId);
        IEnumerable<Store> GetAllStoresForAParticularBranch(long branchId);
    }
}
