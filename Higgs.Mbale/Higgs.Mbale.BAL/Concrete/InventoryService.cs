using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;
using log4net;

namespace Higgs.Mbale.BAL.Concrete
{
 public   class InventoryService : IInventoryService
    {
     ILog logger = log4net.LogManager.GetLogger(typeof(InventoryService));
        private IInventoryDataService _dataService;
        private IUserService _userService;
        private ITransactionDataService _transactionDataService;
        private ITransactionSubTypeService _transactionSubTypeService;
        

        public InventoryService(IInventoryDataService dataService,IUserService userService,ITransactionDataService transactionDataService,ITransactionSubTypeService transactionSubTypeService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionDataService = transactionDataService;
            this._transactionSubTypeService = transactionSubTypeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <returns></returns>
        public Inventory GetInventory(long inventoryId)
        {
            var result = this._dataService.GetInventory(inventoryId);
            return MapEFToModel(result);
        }
        public IEnumerable<Inventory> GetAllInventoriesForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllInventoriesForAParticularBranch(branchId);
            return MapEFToModel(results);
        }

        public IEnumerable<Inventory> GetAllInventoriesForAParticularStore(long storeId)
        {
            var results = this._dataService.GetAllInventoriesForAParticularStore(storeId);
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Inventory> GetAllInventories()
        {
            var results = this._dataService.GetAllInventories();
            return MapEFToModel(results);
        } 

       
        public long SaveInventory(Inventory inventory, string userId)
        {
            var inventoryDTO = new DTO.InventoryDTO()
            {
                ItemName = inventory.ItemName,
                Description = inventory.Description,
                PurchaseDate = inventory.PurchaseDate,
                Amount = inventory.Amount,
                BranchId = inventory.BranchId,
                SectorId = inventory.SectorId,
                StoreId = inventory.StoreId,
                TransactionSubTypeId = inventory.TransactionSubTypeId,
                InventoryId = inventory.InventoryId,
                Deleted = inventory.Deleted,
                CreatedBy = inventory.CreatedBy,
                CreatedOn = inventory.CreatedOn

            };

           var InventoryId = this._dataService.SaveInventory(inventoryDTO, userId);

           if (inventory.InventoryId == 0)
           {
               long transactionTypeId = 0;
               var transactionSubtype = _transactionSubTypeService.GetTransactionSubType(inventoryDTO.TransactionSubTypeId);
               if (transactionSubtype != null)
               {
                   transactionTypeId = transactionSubtype.TransactionTypeId;
               }

               var transaction = new TransactionDTO()
               {
                   BranchId = inventoryDTO.BranchId,
                   SectorId = inventoryDTO.SectorId,
                   Amount = inventoryDTO.Amount,
                   TransactionSubTypeId = inventoryDTO.TransactionSubTypeId,
                   TransactionTypeId = transactionTypeId,
                   CreatedOn = DateTime.Now,
                   TimeStamp = DateTime.Now,
                   CreatedBy = userId,
                   Deleted = false,

               };
               var transactionId = _transactionDataService.SaveTransaction(transaction, userId);
           }
           
           return InventoryId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long inventoryId, string userId)
        {
            _dataService.MarkAsDeleted(inventoryId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Inventory> MapEFToModel(IEnumerable<EF.Models.Inventory> data)
        {
            var list = new List<Inventory>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Inventory EF object to Inventory Model Object and
        /// returns the Inventory model object.
        /// </summary>
        /// <param name="result">EF Inventory object to be mapped.</param>
        /// <returns>Inventory Model Object.</returns>
        public Inventory MapEFToModel(EF.Models.Inventory data)
        {
          
            var inventory = new Inventory()
            {
                ItemName = data.ItemName,
                Amount = data.Amount,
                Description = data.Description,
                PurchaseDate = data.PurchaseDate,
                BranchName = data.Branch !=null? data.Branch.Name:"",
                SectorName = data.Sector != null ? data.Sector.Name : "",
                TransactionSubTypeId = data.TransactionSubTypeId,
                TransactionSubTypeName = data.TransactionSubType !=null?data.TransactionSubType.Name:"",
                StoreName = data.Store != null?data.Store.Name:"",
                BranchId = data.BranchId,
                StoreId = data.StoreId,
                SectorId = data.SectorId,
                InventoryId = data.InventoryId,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),               

            };
            return inventory;
        }



       #endregion
    }
}
