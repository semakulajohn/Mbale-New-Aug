using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface ITransactionService
    {
     IEnumerable<Transaction> GetAllTransactionsForAParticularTransactionType(long transactionTypeId);
     IEnumerable<Transaction> GetAllTransactions();
     IEnumerable<Transaction> GetAllTransactionsForAParticularBranch(long branchId);
     Transaction GetTransaction(long transactionId);
     long SaveTransaction(Transaction transaction, string userId);
     IEnumerable<Transaction> MapEFToModel(IEnumerable<EF.Models.Transaction> data);
    }
}
