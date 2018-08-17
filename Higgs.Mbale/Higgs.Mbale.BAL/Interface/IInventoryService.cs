using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IInventoryService
    {
         IEnumerable<Inventory> GetAllInventories();
        Inventory GetInventory(long inventoryId);
        long SaveInventory(Inventory inventory, string userId);
        void MarkAsDeleted(long inventoryId, string userId);
        IEnumerable<Inventory> GetAllInventoriesForAParticularBranch(long branchId);
        IEnumerable<Inventory> GetAllInventoriesForAParticularStore(long storeId);
    
    }
}
