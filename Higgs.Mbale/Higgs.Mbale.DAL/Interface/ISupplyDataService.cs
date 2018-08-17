using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
    public interface ISupplyDataService
    {
        IEnumerable<Supply> GetAllSupplies();
        Supply GetSupply(long supplyId);
        IEnumerable<Supply> GetAllSuppliesForAParticularSupplier(string supplierId);
        long SaveSupply(SupplyDTO supply, string userId);
        void MarkAsDeleted(long supplyId, string userId);
        IEnumerable<Supply> GetAllSuppliesForAParticularBranch(long branchId);
        IEnumerable<Supply> GetAllSuppliesToBeUsed();
        IEnumerable<Supply> GetAllUnPaidSuppliesForAParticularSupplier(string supplierId);
        IEnumerable<Supply> GetAllPaidSuppliesForAParticularSupplier(string supplierId);
        void UpdateBatchSupplyWithCompletedStatus(long supplyId, long statusId, string userId);

        void SaveStoreMaizeStock(StoreMaizeStockDTO storeMaizeStockDTO);

        StoreMaizeStock GetLatestMaizeStockForAParticularStore(long storeId);

        IEnumerable<StoreMaizeStock> GetMaizeStocksForAParticularStore(long storeId);
    }
  
}
