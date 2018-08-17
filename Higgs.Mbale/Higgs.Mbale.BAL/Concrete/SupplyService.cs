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
using System.Configuration;

namespace Higgs.Mbale.BAL.Concrete
{
  public  class SupplyService : ISupplyService
    {
      private string supplyTransactionSubTypeId = ConfigurationManager.AppSettings["SupplyTransactionSubTypeId"];
      private string sectorId = ConfigurationManager.AppSettings["SectorId"];

         ILog logger = log4net.LogManager.GetLogger(typeof(SupplyService));
        private ISupplyDataService _dataService;
        private IUserService _userService;
        private IAccountTransactionActivityService _accountTransactionActivityService;
        private ICreditorService _creditorService;
        

        public SupplyService(ISupplyDataService dataService,IUserService userService,IAccountTransactionActivityService accountTransactionActivityService,
            ICreditorService creditorService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._accountTransactionActivityService = accountTransactionActivityService;
            this._creditorService = creditorService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplyId"></param>
        /// <returns></returns>
        public Supply GetSupply(long supplyId)
        {
            var result = this._dataService.GetSupply(supplyId);
            return MapEFToModel(result);
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Supply> GetAllSupplies()
        {
            var results = this._dataService.GetAllSupplies();
            return MapEFToModel(results);
        }

        public long MakeSupplyPayment(MultipleSupplies model, AccountTransactionActivity accountActivity, string userId)
        {
            long accountActivityId = 0;
            string supplierId = "";
            if (model.Supplies.Any())
            {
                foreach (var supply in model.Supplies)
                {
                    supplierId = supply.SupplierId;
                    var supplyObject = new SupplyDTO()
                    {
                        SupplyId = supply.SupplyId,
                        SupplierId = supply.SupplierId,
                        SupplyNumber = supply.SupplyNumber,
                        Price = supply.Price,
                        Amount = supply.Amount,
                        StoreId = supply.StoreId,
                        BranchId = supply.BranchId,
                        SupplyDate = supply.SupplyDate,
                        TruckNumber = supply.TruckNumber,
                        WeightNoteNumber = supply.WeightNoteNumber,
                        Used = supply.Used,
                        IsPaid = true,
                        StatusId = supply.StatusId,
                        Deleted = supply.Deleted,
                        MoistureContent = supply.MoistureContent,
                        BagsOfStones = supply.BagsOfStones,
                        NormalBags = supply.NormalBags,
                        Quantity = supply.Quantity,
                    };
                   _dataService.SaveSupply(supplyObject, userId);
                }

                UpdateCreditorRecords(supplierId,accountActivity.Amount,userId,accountActivity.BranchId);

                var accountActivityObject = new AccountTransactionActivity()
                {
                    AspNetUserId = accountActivity.AspNetUserId,
                    CasualWorkerId = accountActivity.CasualWorkerId,
                    Amount = accountActivity.Amount,
                    Notes = accountActivity.Notes,
                    AccountTransactionActivityId = accountActivity.AccountTransactionActivityId,
                    Action = accountActivity.Action,
                    BranchId = accountActivity.BranchId,
                    TransactionSubTypeId = accountActivity.TransactionSubTypeId,
                    SectorId = accountActivity.SectorId,
                    Deleted = accountActivity.Deleted,
                    CreatedBy = accountActivity.CreatedBy,
                    CreatedOn = accountActivity.CreatedOn
                };
                accountActivityId = _accountTransactionActivityService.SaveAccountTransactionActivity(accountActivityObject, userId);
            }
            return accountActivityId;
        }

        private void UpdateCreditorRecords(string aspNetUserId,double amount,string userId,long branchId)
        {
            double creditBalance=0,creditTotal=0;
            var creditorRecords = _creditorService.GetAllCreditorRecordsForParticularAccount(aspNetUserId);
            if (creditorRecords.Any())
            {
                foreach (var creditorRecord in creditorRecords)
                {
                    creditTotal = creditorRecord.Amount + creditTotal;
                    var creditor = new Creditor()
                    {
                        CreditorId = creditorRecord.CreditorId,
                        AspNetUserId = creditorRecord.AspNetUserId,
                        Action = true,
                        Amount = creditorRecord.Amount,
                        BranchId = creditorRecord.BranchId,
                        SectorId = creditorRecord.SectorId,
                        Deleted = creditorRecord.Deleted,
                        CreatedBy = creditorRecord.CreatedBy,
                        CreatedOn = creditorRecord.CreatedOn,
                        TimeStamp = creditorRecord.TimeStamp,


                    };
                    var creditorId = _creditorService.SaveCreditor(creditor, userId);
                }
                creditBalance =  creditTotal -amount ;
                if (creditBalance > 0)
                {
                    var creditor = new Creditor()
                    {
                        AspNetUserId = aspNetUserId,
                        Action = false,
                        Amount = creditBalance,
                        BranchId = branchId,
                        SectorId = Convert.ToInt64(sectorId),
                        Deleted = false,
                        CreatedBy = userId,
                       

                    };
                    var creditorId = _creditorService.SaveCreditor(creditor, userId);
                }

            }
           
        }     
        public IEnumerable<Supply> GetAllSuppliesToBeUsed()
        {
            var results = this._dataService.GetAllSuppliesToBeUsed();
            return MapEFToModel(results);
        }
        public IEnumerable<Supply> GetAllSuppliesForAParticularSupplier(string supplierId)
        {
            var results = this._dataService.GetAllSuppliesForAParticularSupplier(supplierId);
            return MapEFToModel(results);
        }

        public IEnumerable<Supply> GetAllUnPaidSuppliesForAParticularSupplier(string supplierId)
        {
            var results = this._dataService.GetAllUnPaidSuppliesForAParticularSupplier(supplierId);
            return MapEFToModel(results);
        }
        public IEnumerable<Supply> GetAllPaidSuppliesForAParticularSupplier(string supplierId)
        {
            var results = this._dataService.GetAllPaidSuppliesForAParticularSupplier(supplierId);
            return MapEFToModel(results);
        }

        public IEnumerable<Supply> GetAllSuppliesForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllSuppliesForAParticularBranch(branchId);
            return MapEFToModel(results);
        }
        public long SaveSupply(Supply supply, string userId)
        {
            var amount = (supply.Price * supply.Quantity);

            var supplyDTO = new DTO.SupplyDTO()
            {
                Quantity = supply.Quantity,
                SupplyDate = supply.SupplyDate,
                SupplyNumber = supply.SupplyNumber,
                BranchId = supply.BranchId,
                SupplierId = supply.SupplierId,
                Amount = amount,
                TruckNumber = supply.TruckNumber,
                Used = supply.Used,
                SupplyId = supply.SupplyId,
                WeightNoteNumber = supply.WeightNoteNumber,
                MoistureContent = supply.MoistureContent,
                NormalBags = supply.NormalBags,
                BagsOfStones = supply.BagsOfStones,
                Price = supply.Price,
                IsPaid = supply.IsPaid,
                StatusId = supply.StatusId,
                CreatedOn = supply.CreatedOn,
                TimeStamp = supply.TimeStamp,
                CreatedBy = supply.CreatedBy,
                Deleted = supply.Deleted,
                StoreId = supply.StoreId,
              
            };

           var supplyId = this._dataService.SaveSupply(supplyDTO, userId);

           var storeMaizeStock = new StoreMaizeStock()
           {
               SupplyId = supplyId,
               Quantity = supply.Quantity,
               StoreId = supply.StoreId,
               BranchId = supply.BranchId,
               SectorId = Convert.ToInt64(sectorId),
              
           };
           SaveStoreMaizeStock(storeMaizeStock, true);
           var notes = "Maize supply";
           var accountActivity = new AccountTransactionActivity()
            {
                
                AspNetUserId = supply.SupplierId,
                    
                    Amount = amount,
                    Notes = notes,
                    
                    Action = "+",
                    BranchId = supply.BranchId,
                TransactionSubTypeId = Convert.ToInt64(supplyTransactionSubTypeId),
                    SectorId = Convert.ToInt64(sectorId),
                    Deleted = supply.Deleted,
                    CreatedBy = userId,
                    
           };
           var accountActivityId = this._accountTransactionActivityService.SaveAccountTransactionActivity(accountActivity, userId);
           return supplyId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplyId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long SupplyId, string userId)
        {
            _dataService.MarkAsDeleted(SupplyId, userId);
        }

        private double GetStockMaizeBalanceForLastSupplyTransaction(long storeId)
        {
            double balance = 0;

            var result = this._dataService.GetLatestMaizeStockForAParticularStore(storeId);
            if (result.StoreMaizeStockId > 0)
            {
                balance = result.StockBalance;
            }

            return balance;

        }

        public IEnumerable<StoreMaizeStock> GetMaizeStocksForAParticularStore(long storeId)
        {
            var results = this._dataService.GetMaizeStocksForAParticularStore(storeId);
            return MapEFToModel(results);
        }

        public void SaveStoreMaizeStock(StoreMaizeStock storeMaizeStock, bool inOrOut)
        {

            double startStock = 0;
            double OldMaizeStockBalance = 0;
            double NewMaizeStockBalance = 0;


            OldMaizeStockBalance = GetStockMaizeBalanceForLastSupplyTransaction(storeMaizeStock.StoreId);
            startStock = OldMaizeStockBalance;


            if (inOrOut == true)
            {
                NewMaizeStockBalance = OldMaizeStockBalance + storeMaizeStock.Quantity;
            }
            else
            {
                NewMaizeStockBalance = OldMaizeStockBalance - storeMaizeStock.Quantity;
            }

            var storeMaizeStockDTO = new DTO.StoreMaizeStockDTO()
            {
                StoreMaizeStockId = storeMaizeStock.StoreMaizeStockId,
                StoreId = storeMaizeStock.StoreId,
                StartStock = startStock,
                SupplyId = storeMaizeStock.SupplyId,
                StockBalance = NewMaizeStockBalance,
                BranchId = storeMaizeStock.BranchId,
                Quantity = storeMaizeStock.Quantity,
                SectorId = storeMaizeStock.SectorId,
                TimeStamp = storeMaizeStock.TimeStamp,

            };

            this._dataService.SaveStoreMaizeStock(storeMaizeStockDTO);
        }
            
        #region Mapping Methods

        public IEnumerable<Supply> MapEFToModel(IEnumerable<EF.Models.Supply> data)
        {
            var list = new List<Supply>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Supply EF object to Supply Model Object and
        /// returns the Supply model object.
        /// </summary>
        /// <param name="result">EF Supply object to be mapped.</param>
        /// <returns>Supply Model Object.</returns>
        public Supply MapEFToModel(EF.Models.Supply data)
        {
            
            var Supply = new Supply()
            {
                Quantity = data.Quantity,
                SupplyDate = data.SupplyDate,
                SupplyId =data.SupplyId,
                SupplyNumber = data.SupplyNumber,
                BranchId = data.BranchId,
                SupplierId = data.SupplierId,
                Amount = data.Amount,
                TruckNumber = data.TruckNumber,
                Price = data.Price,
                Used =data.Used,
                StoreId = data.StoreId,
                StoreName = data.Store != null?data.Store.Name:"",
                StatusId = data.StatusId,
                IsPaid = data.IsPaid,
                NormalBags = data.NormalBags,
                BagsOfStones = data.BagsOfStones,
                MoistureContent = data.MoistureContent,
                WeightNoteNumber = data.WeightNoteNumber,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted, 
                StatusName = data.Status != null? data.Status.Name:"",
                SupplierName = _userService.GetUserFullName(data.AspNetUser2),
                BranchName = data.Branch !=null? data.Branch.Name:"",
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),               

            };
            return Supply;
        }
      
        public IEnumerable<StoreMaizeStock> MapEFToModel(IEnumerable<EF.Models.StoreMaizeStock> data)
        {
            var list = new List<StoreMaizeStock>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps StoreMaizeStock EF object to StoreMaizeStock Model Object and
        /// returns the StoreMaizeStock model object.
        /// </summary>
        /// <param name="result">EF StoreMaizeStock object to be mapped.</param>
        /// <returns>StoreMaizeStock Model Object.</returns>
        public StoreMaizeStock MapEFToModel(EF.Models.StoreMaizeStock data)
        {

            var storeMaizeStock = new StoreMaizeStock()
            {
                Quantity = data.Quantity,
               StockBalance = data.StockBalance,
               StartStock = data.StartStock,
                SupplyId = data.SupplyId,
                SupplyNumber = data.Supply != null ? Convert.ToString(data.Supply.SupplyNumber) :"",
                BranchId = data.BranchId,
                StoreId = data.StoreId,
                StoreName = data.Store != null ? data.Store.Name : "",
                TimeStamp = data.TimeStamp,
                BranchName = data.Branch != null ? data.Branch.Name : "",
                SectorId = data.SectorId,
                SectorName = data.Sector != null? data.Sector.Name : "",
                StoreMaizeStockId  = data.StoreMaizeStockId,
            };
            return storeMaizeStock;
        }


       #endregion
    }
}
