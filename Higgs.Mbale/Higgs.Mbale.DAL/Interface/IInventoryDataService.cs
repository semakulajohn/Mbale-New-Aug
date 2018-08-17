using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.DTO;

namespace Higgs.Mbale.DAL.Interface
{
public    interface IInventoryDataService
    {
        IEnumerable<Inventory> GetAllInventories();
        Inventory GetInventory(long inventoryId);
        long SaveInventory(InventoryDTO inventory, string userId);
        void MarkAsDeleted(long inventoryId, string userId);
        IEnumerable<Inventory> GetAllInventoriesForAParticularBranch(long branchId);
        IEnumerable<Inventory> GetAllInventoriesForAParticularStore(long storeId);

    }
}
