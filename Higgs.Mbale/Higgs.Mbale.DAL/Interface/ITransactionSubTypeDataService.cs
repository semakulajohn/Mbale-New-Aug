using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface ITransactionSubTypeDataService
    {
        IEnumerable<TransactionSubType> GetAllTransactionSubTypes();
        TransactionSubType GetTransactionSubType(long transactionSubTypeId);
        long SaveTransactionSubType(TransactionSubTypeDTO transactionSubType, string userId);
        void MarkAsDeleted(long transactionSubTypeId, string userId);
        IEnumerable<TransactionType> GetAllTransactionTypes();
    }
}
