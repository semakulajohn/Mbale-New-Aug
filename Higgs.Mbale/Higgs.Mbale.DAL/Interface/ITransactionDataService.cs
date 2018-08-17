using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface ITransactionDataService
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction GetTransaction(long transactionId);
        long SaveTransaction(TransactionDTO transaction, string userId);
        void MarkAsDeleted(long transactionId, string userId);
        IEnumerable<Transaction> GetAllTransactionsForAParticularTransactionType(long transactionTypeId);
        IEnumerable<Transaction> GetAllTransactionsForAParticularBranch(long branchId);
    }
}
