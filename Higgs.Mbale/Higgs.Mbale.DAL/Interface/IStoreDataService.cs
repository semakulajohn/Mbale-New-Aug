using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface IStoreDataService
    {
        IEnumerable<Store> GetAllStores();
        Store GetStore(long storeId);
        long SaveStore(StoreDTO store, string userId);
        void MarkAsDeleted(long storeId, string userId);
        IEnumerable<Store> GetAllStoresForAParticularBranch(long branchId);
    }
}
