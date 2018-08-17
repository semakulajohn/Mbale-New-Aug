using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
public    interface IStockDataService
    {
        IEnumerable<Stock> GetAllStocks();
        Stock GetStock(long stockId);
        long SaveStock(StockDTO stock, string userId);
        void MarkAsDeleted(long stockId, string userId);
        IEnumerable<Stock> GetAllStocksForAParticularBranch(long branchId);
        void SaveStockGrade(StockGradeDTO stockGradeDTO);
        void SaveStockGradeSize(StockGradeSizeDTO stockGradeSizeDTO);
        void PurgeStockGradeSize(long stockId);
       void SaveStockProduct(StockProductDTO stockProductDTO);
       void PurgeStockProduct(long stockId);
       StockProduct GetStockProductForAStock(long stockId);
       StoreStock GetLatestStockForAParticularStore(long storeId, long productId);
       void SaveStockStore(StoreStockDTO storeStockDTO);
       IEnumerable<StoreStock> GetStocksForAParticularStore(long storeId);
       void SaveStoreGradeSize(StoreGradeSizeDTO storeGradeSizeDTO);
        Size GetSize(long sizeId);
        Grade GetGrade(long gradeId);
        IEnumerable<StoreGradeSize> GetStoreGradeSizeForParticularGradeAtAStore(long gradeId, long storeId);
        IEnumerable<StoreGradeSize> GetStoreFlourStock(long storeId);
    }
}
