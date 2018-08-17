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
 public   class DebtorService : IDebtorService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(DebtorService));
        private IDebtorDataService _dataService;
        private IUserService _userService;
        private ITransactionDataService _transactionDataService;
        private ITransactionSubTypeService _transactionSubTypeService;

        

        public DebtorService(IDebtorDataService dataService,IUserService userService,ITransactionDataService transactionDataService,ITransactionSubTypeService transactionSubTypeService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionDataService = transactionDataService;
            this._transactionSubTypeService = transactionSubTypeService;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DebtorId"></param>
        /// <returns></returns>
        public Debtor GetDebtor(long debtorId)
        {
            var result = this._dataService.GetDebtor(debtorId);
            return MapEFToModel(result);
        }

        public IEnumerable<Debtor> GetAllDebtorsForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllDebtorsForAParticularBranch(branchId);
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Debtor> GetAllDebtors()
        {
            var results = this._dataService.GetAllDebtors();
            return MapEFToModel(results);
        } 

       
        public long SaveDebtor(Debtor debtor, string userId)
        {
            var debtorDTO = new DTO.DebtorDTO()
            {
                AspNetUserId = debtor.AspNetUserId,
                Action = debtor.Action,
                Amount = debtor.Amount,
                CasualWorkerId = debtor.CasualWorkerId,
                BranchId = debtor.BranchId,
                SectorId = debtor.SectorId,
                DebtorId = debtor.DebtorId,
                Deleted = debtor.Deleted,
                CreatedBy = debtor.CreatedBy,
                CreatedOn = debtor.CreatedOn

            };

           var debtorId = this._dataService.SaveDebtor(debtorDTO, userId);

           return debtorId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DebtorId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long debtorId, string userId)
        {
            _dataService.MarkAsDeleted(debtorId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Debtor> MapEFToModel(IEnumerable<EF.Models.Debtor> data)
        {
            var list = new List<Debtor>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Debtor EF object to Debtor Model Object and
        /// returns the Debtor model object.
        /// </summary>
        /// <param name="result">EF Debtor object to be mapped.</param>
        /// <returns>Debtor Model Object.</returns>
        public Debtor MapEFToModel(EF.Models.Debtor data)
        {
            var accountName = string.Empty;
            if (data.CasualWorkerId != null)
            {
                accountName = (data.CasualWorker.FirstName + " " + data.CasualWorker.LastName);
            }
            else if(data.AspNetUserId != null)
            {
                accountName = _userService.GetUserFullName(data.AspNetUser);
            }
            var debtor = new Debtor()
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
                DebtorId = data.DebtorId,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser1),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser3),               

            };
            return debtor;
        }



       #endregion
    }
}
