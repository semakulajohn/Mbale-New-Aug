using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DAL.Interface;
using log4net;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.EF.UnitOfWork;
using Higgs.Mbale.DTO;

namespace Higgs.Mbale.DAL.Concrete
{
 public   class TransactionDataService : DataServiceBase,ITransactionDataService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(TransactionDataService));

       public TransactionDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Transaction> GetAllTransactions()
        {
            return this.UnitOfWork.Get<Transaction>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public IEnumerable<Transaction> GetAllTransactionsForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Transaction>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }

        public IEnumerable<Transaction> GetAllTransactionsForAParticularTransactionType(long transactionTypeId)
        {
            return this.UnitOfWork.Get<Transaction>().AsQueryable().Where(e => e.Deleted == false && e.TransactionTypeId == transactionTypeId);
        }
        public Transaction GetTransaction(long transactionId)
        {
            return this.UnitOfWork.Get<Transaction>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.TransactionId == transactionId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new transaction or updates an already existing transaction.
        /// </summary>
        /// <param name="Transaction">Transaction to be saved or updated.</param>
        /// <param name="TransactionId">TransactionId of the Transaction creating or updating</param>
        /// <returns>TransactionId</returns>
        public long SaveTransaction(TransactionDTO transactionDTO, string userId)
        {
            long transactionId = 0;
            
            if (transactionDTO.TransactionId == 0)
            {
           
                var transaction = new Transaction()
                {
                    BranchId = transactionDTO.BranchId,
                    SectorId = transactionDTO.SectorId,
                    Amount = transactionDTO.Amount,
                    TransactionSubTypeId = transactionDTO.TransactionSubTypeId,
                    TransactionTypeId = transactionDTO.TransactionTypeId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<Transaction>().AddNew(transaction);
                this.UnitOfWork.SaveChanges();
                transactionId = transaction.TransactionId;
                return transactionId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Transaction>().AsQueryable()
                    .FirstOrDefault(e => e.TransactionId == transactionDTO.TransactionId);
                if (result != null)
                {
                    result.BranchId = transactionDTO.BranchId;
                    result.Amount = transactionDTO.Amount;
                    result.SectorId = transactionDTO.SectorId;
                    result.TransactionSubTypeId = transactionDTO.TransactionSubTypeId;
                    result.TransactionTypeId = transactionDTO.TransactionTypeId;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = transactionDTO.Deleted;
                    result.DeletedBy = transactionDTO.DeletedBy;
                    result.DeletedOn = transactionDTO.DeletedOn;

                    this.UnitOfWork.Get<Transaction>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return transactionDTO.TransactionId;
            }           
        }

        public void MarkAsDeleted(long transactionId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_Estate_And_RelatedData_AsDeleted(SectorId, userId);
            }

        }
    }
}
