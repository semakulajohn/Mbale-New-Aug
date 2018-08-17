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
using EntityFramework.Extensions;

namespace Higgs.Mbale.DAL.Concrete
{
public   class StockDataService : DataServiceBase,IStockDataService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(StockDataService));

        public StockDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return this.UnitOfWork.Get<Stock>().AsQueryable()
                .Where(e => e.Deleted == false);
        }

        public IEnumerable<Stock> GetAllStocksForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Stock>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }

        public Size GetSize(long sizeId)
        {
            return this.UnitOfWork.Get<Size>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.SizeId == sizeId &&
                    c.Deleted == false
                );
        }

        public Grade GetGrade(long gradeId)
        {
            return this.UnitOfWork.Get<Grade>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.GradeId == gradeId &&
                    c.Deleted == false
                );
        }
        public Stock GetStock(long StockId)
        {
            return this.UnitOfWork.Get<Stock>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.StockId == StockId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new Stock or updates an already existing Stock.
        /// </summary>
        /// <param name="StockDTO">Stock to be saved or updated.</param>
        /// <param name="userId">userId of the Stock creating or updating</param>
        /// <returns>StockId</returns>
        public long SaveStock(StockDTO stockDTO, string userId)
        {
            long stockId = 0;

            if (stockDTO.StockId == 0)
            {
                var stock = new Stock()
                {
                    SectorId = stockDTO.SectorId,
                    BatchId = stockDTO.BatchId,
                    InOrOut = stockDTO.InOrOut,
                    BranchId = stockDTO.BranchId,   
                    StoreId = stockDTO.StoreId,
                    ProductId= stockDTO.ProductId,               
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
                };

                this.UnitOfWork.Get<Stock>().AddNew(stock);
                this.UnitOfWork.SaveChanges();
                stockId = stock.StockId;
                

               
                return stockId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Stock>().AsQueryable()
                    .FirstOrDefault(e => e.StockId == stockDTO.StockId);
                if (result != null)
                {
                    result.StockId = stockDTO.StockId;
                    result.SectorId = stockDTO.SectorId;
                    result.BatchId = stockDTO.BatchId;
                    result.ProductId = stockDTO.ProductId;              
                    result.BranchId = stockDTO.BranchId;
                    result.StoreId = stockDTO.StoreId;
                    result.InOrOut = stockDTO.InOrOut;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    this.UnitOfWork.Get<Stock>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return stockDTO.StockId;
            }
        }

        public void MarkAsDeleted(long StockId, string userId)
        {
            using (var dbContext = new MbaleEntities())
            {
                //TODO: THROW NOT IMPLEMENTED EXCEPTION
            }

        }

        public void SaveStockGrade(StockGradeDTO stockGradeDTO)
        {
            var stockGrade = new StockGrade()
            {
                StockId = stockGradeDTO.StockId,
                GradeId = stockGradeDTO.GradeId,
                TimeStamp = DateTime.Now
            };
            this.UnitOfWork.Get<StockGrade>().AddNew(stockGrade);
            this.UnitOfWork.SaveChanges();
        }

        public void SaveStockGradeSize(StockGradeSizeDTO stockGradeSizeDTO)
        {
            var stockGradeSize = new StockGradeSize()
            {
                StockId = stockGradeSizeDTO.StockId,
                GradeId = stockGradeSizeDTO.GradeId,
                SizeId = stockGradeSizeDTO.SizeId,
                Quantity = stockGradeSizeDTO.Quantity,
                TimeStamp = DateTime.Now
            };
            this.UnitOfWork.Get<StockGradeSize>().AddNew(stockGradeSize);
            this.UnitOfWork.SaveChanges();
        }

        public void PurgeStockGradeSize(long stockId)
        {
            this.UnitOfWork.Get<StockGradeSize>().AsQueryable()
                .Where(m => m.StockId == stockId)
                .Delete();
        }

        public void SaveStockProduct(StockProductDTO stockProductDTO)
        {
            var stockProduct = new StockProduct()
            {
                StockId = stockProductDTO.StockId,
                ProductId = stockProductDTO.ProductId,
                Quantity = stockProductDTO.Quantity,
                TimeStamp = DateTime.Now
            };
            this.UnitOfWork.Get<StockProduct>().AddNew(stockProduct);
            this.UnitOfWork.SaveChanges();
        }
        public void PurgeStockProduct(long stockId)
        {
            this.UnitOfWork.Get<StockProduct>().AsQueryable()
                .Where(m => m.StockId == stockId)
                .Delete();
        }

        public StockProduct GetStockProductForAStock(long stockId)
        {
            return this.UnitOfWork.Get<StockProduct>().AsQueryable()
                  .FirstOrDefault(c =>
                     c.StockId == stockId
                 );
        }

        public void SaveStockStore(StoreStockDTO storeStockDTO)
        {
            
            var storeStock = new StoreStock()
            {
                StockId = storeStockDTO.StockId,
                StockBalance = storeStockDTO.StockBalance,
                ProductId = storeStockDTO.ProductId,
                StoreId = storeStockDTO.StoreId,
                BranchId = storeStockDTO.BranchId,
                StartStock = storeStockDTO.StartStock,
                SectorId = storeStockDTO.SectorId,
                Quantity = storeStockDTO.Quantity,
                TimeStamp = DateTime.Now
            };
            this.UnitOfWork.Get<StoreStock>().AddNew(storeStock);
            this.UnitOfWork.SaveChanges();
        }

        public StoreStock GetLatestStockForAParticularStore(long storeId,long productId)
        {
            StoreStock storeStock = new StoreStock();
            var storeStocks = this.UnitOfWork.Get<StoreStock>().AsQueryable().Where(e => e.StoreId == storeId && e.ProductId == productId);
            if (storeStocks.Any())
            {
                storeStock = storeStocks.AsQueryable().OrderByDescending(e => e.TimeStamp).First();
                return storeStock;
            }
            else
            {
                return storeStock;
            }

        }

        public IEnumerable<StoreStock> GetStocksForAParticularStore(long storeId)
        {
            return  this.UnitOfWork.Get<StoreStock>().AsQueryable().Where(e => e.StoreId == storeId);           

        }

        public void SaveStoreGradeSize(StoreGradeSizeDTO storeGradeSizeDTO)
        {
            double sizeQuantity = 0;
            var result = this.UnitOfWork.Get<StoreGradeSize>().AsQueryable()
           .FirstOrDefault(e => e.StoreId == storeGradeSizeDTO.StoreId && e.GradeId == storeGradeSizeDTO.GradeId && e.SizeId == storeGradeSizeDTO.SizeId);
           

            if (result == null)
            {
                var storeGradeSize = new StoreGradeSize()
                {

                    GradeId = storeGradeSizeDTO.GradeId,
                    SizeId = storeGradeSizeDTO.SizeId,
                    StoreId = storeGradeSizeDTO.StoreId,
                    Quantity = storeGradeSizeDTO.Quantity,
                    TimeStamp = DateTime.Now
                };
                this.UnitOfWork.Get<StoreGradeSize>().AddNew(storeGradeSize);
                this.UnitOfWork.SaveChanges();


            }

            else
            {
            sizeQuantity = result.Quantity + storeGradeSizeDTO.Quantity;

            result.StoreId = storeGradeSizeDTO.StoreId;
            result.SizeId = storeGradeSizeDTO.SizeId;
            result.GradeId = storeGradeSizeDTO.GradeId;
            result.Quantity = sizeQuantity;
            result.TimeStamp = DateTime.Now;
            this.UnitOfWork.Get<StoreGradeSize>().Update(result);
            this.UnitOfWork.SaveChanges();
                
               
            }
        }

        public IEnumerable<StoreGradeSize> GetStoreGradeSizeForParticularGradeAtAStore(long gradeId,long storeId)
        {
            return this.UnitOfWork.Get<StoreGradeSize>().AsQueryable().Where(e => e.GradeId == gradeId && e.StoreId == storeId);

        }
        public IEnumerable<StoreGradeSize> GetStoreFlourStock(long storeId)
        {
            return this.UnitOfWork.Get<StoreGradeSize>().AsQueryable().Where(e =>e.StoreId == storeId);

        }
        
    }
}
