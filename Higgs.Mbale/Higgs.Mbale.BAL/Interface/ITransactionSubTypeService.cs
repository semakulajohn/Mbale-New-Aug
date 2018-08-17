using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface ITransactionSubTypeService
    {
     IEnumerable<TransactionSubType> GetAllTransactionSubTypes();
     TransactionSubType GetTransactionSubType(long transactionSubTypeId);
     long SaveTransactionSubType(TransactionSubType transactionSubType, string userId);
     void MarkAsDeleted(long transactionSubTypeId, string userId);
     IEnumerable<TransactionType> GetAllTransactionTypes();
    }
}
