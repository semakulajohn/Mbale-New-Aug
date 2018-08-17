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
 public   class AccountTransactionActivityService : IAccountTransactionActivityService
    {
     
        ILog logger = log4net.LogManager.GetLogger(typeof(AccountTransactionActivityService));
        private IAccountTransactionActivityDataService _dataService;
        private IUserService _userService;
        private ITransactionSubTypeService _transactionSubTypeService;
        private ICreditorDataService _creditorDataService;
        private IDebtorDataService _debtorDataService;
        private ITransactionDataService _transactionDataService;
       


        public AccountTransactionActivityService(IAccountTransactionActivityDataService dataService, IUserService userService,
            ITransactionSubTypeService transactionSubTypeService,IDebtorDataService debtorDataService,ICreditorDataService creditorDataService,
            ITransactionDataService transactionDataService)
            
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionSubTypeService = transactionSubTypeService;
            this._creditorDataService = creditorDataService;
            this._debtorDataService = debtorDataService;
            this._transactionDataService = transactionDataService;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccountTransactionActivityId"></param>
        /// <returns></returns>
        public AccountTransactionActivity GetAccountTransactionActivity(long accountTransactionActivityId)
        {
            var result = this._dataService.GetAccountTransactionActivity(accountTransactionActivityId);
            return MapEFToModel(result);
        }

     private bool checkIfUserIsAspNetUser(string accountId)
     {
         var isAspNetUser = false;
         var user = _userService.GetAspNetUser(accountId);
         if (user != null)
         {
             isAspNetUser = true;
             
         }
         return isAspNetUser;
     }
        public IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivitiesForAParticularAccount(string accountId)
        {
            
            var isAspNetUser = checkIfUserIsAspNetUser(accountId);
            if (isAspNetUser)
            {
                var results = this._dataService.GetAllAccountTransactionActivitiesForAParticularAspNetUser(accountId);
                return MapEFToModel(results);
            }
            else
            {
                var casualWorkerId = Convert.ToInt64(accountId);
                var results = this._dataService.GetAllAccountTransactionActivitiesForAParticularCasualWorker(casualWorkerId);
                return MapEFToModel(results);
            }
            
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivities()
        {
            var results = this._dataService.GetAllAccountTransactionActivities();
            return MapEFToModel(results);
        }

        private double GetBalanceForLastAccountAccountTransactionActivity(string accountId)
        {
            double balance = 0;
            var isAspNetUser = checkIfUserIsAspNetUser(accountId);
            if (isAspNetUser)
            {
                var result = this._dataService.GetLatestAccountTransactionActivityForAParticularAspNetUser(accountId);
                if (result.AccountTransactionActivityId > 0)
                {
                    balance = result.Balance;
                }
               
                return balance;
            }
            else
            {
                var casualWorkerId = Convert.ToInt64(accountId);
                var result = this._dataService.GetLatestAccountTransactionActivitiesForAParticularCasualWorker(casualWorkerId);
                if(result.AccountTransactionActivityId > 0){
                    balance = result.Balance;
                }
                return balance;
            }
        }

       
        public long SaveAccountTransactionActivity(AccountTransactionActivity accountTransactionActivity, string userId)
        {
            long accountTransactionActivityId = 0;
            double startAmount =0;
            double OldBalance = 0;
            double NewBalance = 0;
            
                if(accountTransactionActivity.AspNetUserId != null)
                {
                    OldBalance = GetBalanceForLastAccountAccountTransactionActivity(accountTransactionActivity.AspNetUserId);
                    startAmount = OldBalance;
                }
                else{
                    
                    OldBalance = GetBalanceForLastAccountAccountTransactionActivity(Convert.ToString(accountTransactionActivity.CasualWorkerId));
                    startAmount = OldBalance;
                }

                if (accountTransactionActivity.Action == "-")
                {
                    NewBalance = OldBalance - accountTransactionActivity.Amount;
                }
                else
                {
                    NewBalance = OldBalance + accountTransactionActivity.Amount;
                }

                var accountTransactionActivityDTO = new DTO.AccountTransactionActivityDTO()
                {
                    AspNetUserId = accountTransactionActivity.AspNetUserId,
                    CasualWorkerId = accountTransactionActivity.CasualWorkerId,
                    Amount = accountTransactionActivity.Amount,
                    StartAmount = startAmount,
                    Balance = NewBalance,
                    Notes = accountTransactionActivity.Notes,
                    AccountTransactionActivityId = accountTransactionActivity.AccountTransactionActivityId,
                    Action = accountTransactionActivity.Action,
                    BranchId = accountTransactionActivity.BranchId,
                    TransactionSubTypeId = accountTransactionActivity.TransactionSubTypeId,
                    SectorId = accountTransactionActivity.SectorId,
                    Deleted = accountTransactionActivity.Deleted,
                    CreatedBy = accountTransactionActivity.CreatedBy,
                    CreatedOn = accountTransactionActivity.CreatedOn

                };

                 accountTransactionActivityId = this._dataService.SaveAccountTransactionActivity(accountTransactionActivityDTO, userId);

                 long transactionTypeId = 0;
                 List<string> transactionSubTypeNames = new List<string>();
                 var transactionSubType = _transactionSubTypeService.GetTransactionSubType(accountTransactionActivity.TransactionSubTypeId);

                 if (transactionSubType != null)
                 {
                     transactionTypeId = transactionSubType.TransactionTypeId;
                     if(transactionSubType.Name == "Credit" ||transactionSubType.Name =="Advance")
                     {
                         var debtorDTO = new DTO.DebtorDTO()
                         {
                             AspNetUserId = accountTransactionActivity.AspNetUserId,
                             Action = false,
                             Amount = accountTransactionActivity.Amount,
                             CasualWorkerId = accountTransactionActivity.CasualWorkerId,
                             BranchId = accountTransactionActivity.BranchId,
                             SectorId = accountTransactionActivity.SectorId,
                             Deleted = accountTransactionActivity.Deleted,
                             CreatedBy = accountTransactionActivity.CreatedBy,
                             CreatedOn = accountTransactionActivity.CreatedOn

                         };
                         var debtorId = _debtorDataService.SaveDebtor(debtorDTO, userId);
                     }
                     else if (transactionSubType.Name == "Deposit" || transactionSubType.Name == "maize purchase" || transactionSubType.Name == "Casual labour")
                     {
                         var creditorDTO = new DTO.CreditorDTO()
                         {
                             AspNetUserId = accountTransactionActivity.AspNetUserId,
                             Action = false,
                             Amount = accountTransactionActivity.Amount,
                             CasualWorkerId = accountTransactionActivity.CasualWorkerId,
                             BranchId = accountTransactionActivity.BranchId,
                             SectorId = accountTransactionActivity.SectorId,
                             Deleted = accountTransactionActivity.Deleted,
                             CreatedBy = accountTransactionActivity.CreatedBy,
                             CreatedOn = accountTransactionActivity.CreatedOn

                         };
                         var creditorId = _creditorDataService.SaveCreditor(creditorDTO, userId);
                        
                     }
                     var transaction = new TransactionDTO()
                     {
                         BranchId = accountTransactionActivity.BranchId,
                         SectorId = accountTransactionActivity.SectorId,
                         Amount = accountTransactionActivity.Amount,
                         TransactionSubTypeId = accountTransactionActivity.TransactionSubTypeId,
                         TransactionTypeId = transactionTypeId,
                         CreatedOn = DateTime.Now,
                         TimeStamp = DateTime.Now,
                         CreatedBy = userId,
                         Deleted = false,

                     };
                     var transactionId = _transactionDataService.SaveTransaction(transaction, userId);
                 }
            return accountTransactionActivityId;
                 }

        public IEnumerable<PaymentMode> GetAllPaymentModes()
        {
            var results = this._dataService.GetAllPaymentModes();
            return MapEFToModel(results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountTransactionActivityId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long accountTransactionActivityId, string userId)
        {
            _dataService.MarkAsDeleted(accountTransactionActivityId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<AccountTransactionActivity> MapEFToModel(IEnumerable<EF.Models.AccountTransactionActivity> data)
        {
            var list = new List<AccountTransactionActivity>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }


        public AccountTransactionActivity MapEFToModel(EF.Models.AccountTransactionActivity data)
        {
            var accountName = string.Empty;
            
            if (data.AspNetUser != null)
            {
                              
              accountName = _userService.GetUserFullName(data.AspNetUser);
             
            }
            else
            {
                accountName = data.CasualWorker.FirstName + ' ' + data.CasualWorker.LastName;

            }

            var accountTransactionActivity = new AccountTransactionActivity()
            {
                AspNetUserId = data.AspNetUserId,
                CasualWorkerId = data.CasualWorkerId,
                Action = data.Action,
                StartAmount= data.StartAmount,
                Balance = data.Balance,
                Amount = data.Amount,
                Notes = data.Notes,
                AccountTransactionActivityId = data.AccountTransactionActivityId,
                BranchId = data.BranchId,
                BranchName = data.Branch != null?data.Branch.Name:"",
                SectorId = data.SectorId,
                SectorName  = data.Sector != null?data.Sector.Name:"",
                TransactionSubTypeId = data.TransactionSubTypeId,
                TransactionSubTypeName = data.TransactionSubType !=null?data.TransactionSubType.Name:"",
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                AccountName = accountName,
                            

            };
            return accountTransactionActivity;
        }

     
       #endregion

        #region paymentModes
        private IEnumerable<PaymentMode> MapEFToModel(IEnumerable<EF.Models.PaymentMode> data)
        {
            var list = new List<PaymentMode>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        private PaymentMode MapEFToModel(EF.Models.PaymentMode data)
        {
            var paymentMode = new PaymentMode()
            {
                Name = data.Name,
                PaymentModeId = data.PaymentModeId,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                
            };
            return paymentMode;
        }
        #endregion
    }
}
