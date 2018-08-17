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
using System.Configuration;

namespace Higgs.Mbale.DAL.Concrete
{
  public  class SupplyDataService : DataServiceBase,ISupplyDataService
    {
      private string supplyStatusId = ConfigurationManager.AppSettings["SupplyStatusId"];
        ILog logger = log4net.LogManager.GetLogger(typeof(SupplyDataService));

       public SupplyDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
              
        public IEnumerable<Supply> GetAllSupplies()
        {
            return this.UnitOfWork.Get<Supply>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public IEnumerable<Supply> GetAllSuppliesToBeUsed()
        {
            return this.UnitOfWork.Get<Supply>().AsQueryable().Where(e => e.Deleted == false && e.Used == false);
        }
        public   IEnumerable<Supply> GetAllSuppliesForAParticularSupplier(string supplierId)
        {
                 return this.UnitOfWork.Get<Supply>().AsQueryable().Where(e=>e.Deleted ==false && e.SupplierId == supplierId);
         }

        public IEnumerable<Supply> GetAllUnPaidSuppliesForAParticularSupplier(string supplierId)
        {
            return this.UnitOfWork.Get<Supply>().AsQueryable().Where(e => e.Deleted == false && e.SupplierId == supplierId && e.IsPaid ==false);
        }
        public IEnumerable<Supply> GetAllPaidSuppliesForAParticularSupplier(string supplierId)
        {
            return this.UnitOfWork.Get<Supply>().AsQueryable().Where(e => e.Deleted == false && e.SupplierId == supplierId && e.IsPaid == true);
        }
        public IEnumerable<Supply> GetAllSuppliesForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Supply>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }
        public Supply GetSupply(long supplyId)
        {
            return this.UnitOfWork.Get<Supply>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.SupplyId == supplyId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new Supply or updates an already existing Supply.
        /// </summary>
        /// <param name="Supply">Supply to be saved or updated.</param>
        /// <param name="SupplyId">SupplyId of the Supply creating or updating</param>
        /// <returns>SupplyId</returns>
        public long SaveSupply(SupplyDTO supplyDTO, string userId)
        {
            long supplyId = 0;
            
            if (supplyDTO.SupplyId == 0)
            {

                var supply = new Supply()
                {
                     
                    Quantity = supplyDTO.Quantity,
                    SupplyDate =  supplyDTO.SupplyDate,
                    SupplyNumber = supplyDTO.SupplyNumber,
                    BranchId = supplyDTO.BranchId,
                    SupplierId = supplyDTO.SupplierId,
                    Amount = supplyDTO.Amount,
                    TruckNumber = supplyDTO.TruckNumber,
                    Used = supplyDTO.Used,
                    MoistureContent = supplyDTO.MoistureContent,
                    WeightNoteNumber = supplyDTO.WeightNoteNumber,
                    NormalBags = supplyDTO.NormalBags,
                    BagsOfStones = supplyDTO.BagsOfStones,
                    Price = supplyDTO.Price,
                    IsPaid = supplyDTO.IsPaid,
                    StatusId = Convert.ToInt64(supplyStatusId),
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false, 
                    StoreId = supplyDTO.StoreId,
                };

                this.UnitOfWork.Get<Supply>().AddNew(supply);
                this.UnitOfWork.SaveChanges();
                supplyId = supply.SupplyId;
                return supplyId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Supply>().AsQueryable()
                    .SingleOrDefault(e => e.SupplyId == supplyDTO.SupplyId);
                if (result != null)
                {
                    result.Quantity = supplyDTO.Quantity;
                    result.SupplyDate = supplyDTO.SupplyDate;
                    result.SupplyNumber =  supplyDTO.SupplyNumber;
                    result.BranchId = supplyDTO.BranchId;
                    result.SupplierId = supplyDTO.SupplierId;
                    result.Amount = supplyDTO.Amount;
                    result.IsPaid = supplyDTO.IsPaid;
                    result.TruckNumber = supplyDTO.TruckNumber;
                    result.Price = supplyDTO.Price;
                    result.Used = supplyDTO.Used;
                    result.WeightNoteNumber = supplyDTO.WeightNoteNumber;
                    result.BagsOfStones = supplyDTO.BagsOfStones;
                    result.NormalBags = supplyDTO.NormalBags;
                    result.StatusId = supplyDTO.StatusId;
                    result.MoistureContent = supplyDTO.MoistureContent;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = supplyDTO.Deleted;
                    result.DeletedBy = supplyDTO.DeletedBy;
                    result.DeletedOn = supplyDTO.DeletedOn;
                    result.StoreId = supplyDTO.StoreId;

                    this.UnitOfWork.Get<Supply>().Update(result);
                    
                    this.UnitOfWork.SaveChanges();
                }
                return supplyDTO.SupplyId;
            }            
        }

        public void MarkAsDeleted(long supplyId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
              //TODO: THROW NOT IMPLEMENTED EXCEPTION
            }

        }
    public  void UpdateBatchSupplyWithCompletedStatus(long supplyId, long statusId, string userId)
        {
            using (var dbContext = new MbaleEntities())
            {
                dbContext.UpdateSupplyWithCompletedStatus(supplyId, statusId, userId);
            }
            
        }

    public void SaveStoreMaizeStock(StoreMaizeStockDTO storeMaizeStockDTO)
    {

        var storeMaizeStock = new StoreMaizeStock()
        {
            StoreMaizeStockId = storeMaizeStockDTO.StoreMaizeStockId,
            StockBalance = storeMaizeStockDTO.StockBalance,
            SupplyId = storeMaizeStockDTO.SupplyId,
            StoreId = storeMaizeStockDTO.StoreId,
            BranchId = storeMaizeStockDTO.BranchId,
            StartStock = storeMaizeStockDTO.StartStock,
            SectorId = storeMaizeStockDTO.SectorId,
            Quantity = storeMaizeStockDTO.Quantity,
            TimeStamp = DateTime.Now
        };
        this.UnitOfWork.Get<StoreMaizeStock>().AddNew(storeMaizeStock);
        this.UnitOfWork.SaveChanges();
    }

    public StoreMaizeStock GetLatestMaizeStockForAParticularStore(long storeId)
    {
        StoreMaizeStock storeMaizeStock = new StoreMaizeStock();

        var storeMaizeStocks = this.UnitOfWork.Get<StoreMaizeStock>().AsQueryable().Where(e => e.StoreId == storeId);
        if (storeMaizeStocks.Any())
        {
            storeMaizeStock = storeMaizeStocks.AsQueryable().OrderByDescending(e => e.TimeStamp).First();
            return storeMaizeStock;
        }
        else
        {
            return storeMaizeStock;
        }
            
   }

    public IEnumerable<StoreMaizeStock> GetMaizeStocksForAParticularStore(long storeId)
    {
        return this.UnitOfWork.Get<StoreMaizeStock>().AsQueryable().Where(e => e.StoreId == storeId);

    }


    }
}
