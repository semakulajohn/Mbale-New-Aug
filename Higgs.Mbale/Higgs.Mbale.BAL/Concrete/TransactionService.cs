using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;

namespace Higgs.Mbale.BAL.Concrete
{
 public   class TransactionService : ITransactionService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(TransactionService));
        private ITransactionDataService _dataService;
        private IUserService _userService;
        private ITransactionSubTypeService _transactionSubTypeService;


        public TransactionService(ITransactionDataService dataService, IUserService userService, ITransactionSubTypeService transactionSubTypeService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionSubTypeService = transactionSubTypeService;
        }

        public IEnumerable<Transaction> GetAllTransactionsForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllTransactionsForAParticularBranch(branchId);
            return MapEFToModel(results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Transaction> GetAllTransactionsForAParticularTransactionType(long transactionTypeId)
        {
            var results = this._dataService.GetAllTransactionsForAParticularTransactionType( transactionTypeId);
            return MapEFToModel(results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Transaction> GetAllTransactions()
        {
            var results = this._dataService.GetAllTransactions();
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public Transaction GetTransaction(long transactionId)
        {
            var result = this._dataService.GetTransaction(transactionId);
            return MapEFToModel(result);
        }

        public long SaveTransaction(Transaction transaction, string userId)
        {
           
            long transactionTypeId = 0;
            var transactionSubtype = _transactionSubTypeService.GetTransactionSubType(transaction.TransactionSubTypeId);
            if (transactionSubtype != null)
            {
                transactionTypeId = transactionSubtype.TransactionTypeId;
            }

            var transactionDTO = new TransactionDTO()
            {
                BranchId = transaction.BranchId,
                SectorId = transaction.SectorId,
                Amount = transaction.Amount,
                TransactionSubTypeId = transaction.TransactionSubTypeId,
                TransactionTypeId = transactionTypeId,
                CreatedOn = DateTime.Now,
                TimeStamp = DateTime.Now,
                CreatedBy = userId,
                Deleted = false,

            };
            var transactionId = this._dataService.SaveTransaction(transactionDTO, userId);
            return transactionId;
        }


        #region Mapping Methods

        public IEnumerable<Transaction> MapEFToModel(IEnumerable<EF.Models.Transaction> data)
        {
            var list = new List<Transaction>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Transaction EF object to Transaction Model Object and
        /// returns the Transaction model object.
        /// </summary>
        /// <param name="result">EF Transaction object to be mapped.</param>
        /// <returns>Transaction Model Object.</returns>
        public Transaction MapEFToModel(EF.Models.Transaction data)
        {

            var transaction = new Transaction()
            {
                TransactionId = data.TransactionId,
                BranchId = data.BranchId,
                SectorId = data.SectorId,
                Amount = data.Amount,
                TransactionSubTypeId = data.TransactionSubTypeId,
                TransactionTypeName = data.TransactionType != null ? data.TransactionType.Name : "",
                BranchName = data.Branch !=null? data.Branch.Name:"",
                TransactionSubTypeName = data.TransactionSubType != null? data.TransactionSubType.Name: "",
                SectorName = data.Sector != null? data.Sector.Name:"",
                TransactionTypeId = data.TransactionTypeId,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),


            };
            return transaction;
        }


       


        #endregion
    }
}
