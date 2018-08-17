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
  public  class CreditorService : ICreditorService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(CreditorService));
        private ICreditorDataService _dataService;
        private IUserService _userService;
        private ITransactionDataService _transactionDataService;
        private ITransactionSubTypeService _transactionSubTypeService;

        

        public CreditorService(ICreditorDataService dataService,IUserService userService,ITransactionDataService transactionDataService,ITransactionSubTypeService transactionSubTypeService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionDataService = transactionDataService;
            this._transactionSubTypeService = transactionSubTypeService;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CreditorId"></param>
        /// <returns></returns>
        public Creditor GetCreditor(long creditorId)
        {
            var result = this._dataService.GetCreditor(creditorId);
            return MapEFToModel(result);
        }

        public IEnumerable<Creditor> GetAllCreditorsForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllCreditorsForAParticularBranch(branchId);
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Creditor> GetAllCreditors()
        {
            var results = this._dataService.GetAllCreditors();
            return MapEFToModel(results);
        }

      public  IEnumerable<Creditor> GetAllCreditorRecordsForParticularAccount(string aspNetUserId)
        {
            var results = this._dataService.GetAllCreditorRecordsForParticularAccount(aspNetUserId);
            return MapEFToModel(results);
        }
       
        public long SaveCreditor(Creditor creditor, string userId)
        {
            var creditorDTO = new DTO.CreditorDTO()
            {
                AspNetUserId = creditor.AspNetUserId,
                Action = creditor.Action,
                Amount = creditor.Amount,
                CasualWorkerId = creditor.CasualWorkerId,
                BranchId = creditor.BranchId,
                SectorId = creditor.SectorId,
                CreditorId = creditor.CreditorId,
                Deleted = creditor.Deleted,
                CreatedBy = creditor.CreatedBy,
                CreatedOn = creditor.CreatedOn

            };

           var creditorId = this._dataService.SaveCreditor(creditorDTO, userId);
         
           return creditorId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CreditorId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long CreditorId, string userId)
        {
            _dataService.MarkAsDeleted(CreditorId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Creditor> MapEFToModel(IEnumerable<EF.Models.Creditor> data)
        {
            var list = new List<Creditor>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Creditor EF object to Creditor Model Object and
        /// returns the Creditor model object.
        /// </summary>
        /// <param name="result">EF Creditor object to be mapped.</param>
        /// <returns>Creditor Model Object.</returns>
        public Creditor MapEFToModel(EF.Models.Creditor data)
        {
            var accountName = string.Empty;
            if (data.CasualWorkerId != null)
            {
                accountName = (data.CasualWorker.FirstName + " " + data.CasualWorker.LastName);
            }
            else if (data.AspNetUserId != null)
            {
                accountName = _userService.GetUserFullName(data.AspNetUser);
            }
            var creditor = new Creditor()
            {
                                
                BranchName = data.Branch !=null? data.Branch.Name:"",
                SectorName = data.Sector != null ? data.Sector.Name : "",
                AccountName = accountName,
                BranchId = data.BranchId,
                AspNetUserId = data.AspNetUserId,
                CasualWorkerId = data.CasualWorkerId,
                Action = data.Action,
                SectorId = data.SectorId,
                Amount = data.Amount,
                CreditorId = data.CreditorId,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),               

            };
            return creditor;
        }



       #endregion
    }
}
