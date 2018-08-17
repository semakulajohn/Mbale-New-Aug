using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
   public interface ISupplyService
    {

        IEnumerable<Supply> GetAllSupplies();
        Supply GetSupply(long supplyId);
        long SaveSupply(Supply supply, string userId);
        void MarkAsDeleted(long supplyId, string userId);
        IEnumerable<Supply> GetAllSuppliesForAParticularSupplier(string supplierId);
        IEnumerable<Supply> GetAllSuppliesForAParticularBranch(long branchId);
        IEnumerable<Supply> MapEFToModel(IEnumerable<EF.Models.Supply> data);
        IEnumerable<Supply> GetAllSuppliesToBeUsed();
        IEnumerable<Supply> GetAllUnPaidSuppliesForAParticularSupplier(string supplierId);
        IEnumerable<Supply> GetAllPaidSuppliesForAParticularSupplier(string supplierId);
        long MakeSupplyPayment(MultipleSupplies model, AccountTransactionActivity accountActivity, string userId);
       
       IEnumerable<StoreMaizeStock> GetMaizeStocksForAParticularStore(long storeId);

       void SaveStoreMaizeStock(StoreMaizeStock storeMaizeStock, bool inOrOut);
        
    }
}
