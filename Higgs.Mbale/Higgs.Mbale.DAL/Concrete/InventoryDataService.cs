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

namespace Higgs.Mbale.DAL.Concrete
{
 public   class InventoryDataService : DataServiceBase,IInventoryDataService
    {
        
       ILog logger = log4net.LogManager.GetLogger(typeof(InventoryDataService));

       public InventoryDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
              
        public IEnumerable<Inventory> GetAllInventories()
        {
            return this.UnitOfWork.Get<Inventory>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public Inventory GetInventory(long InventoryId)
        {
            return this.UnitOfWork.Get<Inventory>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.InventoryId == InventoryId &&
                    c.Deleted == false
                );
        }
        public IEnumerable<Inventory> GetAllInventoriesForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Inventory>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }

        public IEnumerable<Inventory> GetAllInventoriesForAParticularStore(long storeId)
        {
            return this.UnitOfWork.Get<Inventory>().AsQueryable().Where(e => e.Deleted == false && e.StoreId == storeId);
        }
        /// <summary>
        /// Saves a new Inventory or updates an already existing Inventory.
        /// </summary>
        /// <param name="Inventory">Inventory to be saved or updated.</param>
        /// <param name="InventoryId">InventoryId of the Inventory creating or updating</param>
        /// <returns>InventoryId</returns>
        public long SaveInventory(InventoryDTO inventoryDTO, string userId)
        {
            long inventoryId = 0;
            
            if (inventoryDTO.InventoryId == 0)
            {

                var inventory = new Inventory()
                {
                    ItemName = inventoryDTO.ItemName,
                    Description = inventoryDTO.Description,
                    StoreId = inventoryDTO.StoreId,
                    BranchId = inventoryDTO.BranchId,
                    PurchaseDate = inventoryDTO.PurchaseDate,
                    SectorId = inventoryDTO.SectorId,
                    Amount = inventoryDTO.Amount,
                    TransactionSubTypeId = inventoryDTO.TransactionSubTypeId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false, 
                };

                this.UnitOfWork.Get<Inventory>().AddNew(inventory);
                this.UnitOfWork.SaveChanges();
                inventoryId = inventory.InventoryId;
                return inventoryId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Inventory>().AsQueryable()
                    .FirstOrDefault(e => e.InventoryId == inventoryDTO.InventoryId);
                if (result != null)
                {
                    result.Description = inventoryDTO.Description;
                    result.SectorId = inventoryDTO.SectorId;
                    result.PurchaseDate =  inventoryDTO.PurchaseDate;
                    result.ItemName = inventoryDTO.ItemName;
                    result.TransactionSubTypeId = inventoryDTO.TransactionSubTypeId;
                    result.BranchId = inventoryDTO.BranchId;
                    result.Amount = inventoryDTO.Amount;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = inventoryDTO.Deleted;
                    result.DeletedBy = inventoryDTO.DeletedBy;
                    result.DeletedOn = inventoryDTO.DeletedOn;

                    this.UnitOfWork.Get<Inventory>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return inventoryDTO.InventoryId;
            }            
        }

        public void MarkAsDeleted(long inventoryId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
              //TODO: THROW NOT IMPLEMENTED EXCEPTION
            }

        }
    }
}
