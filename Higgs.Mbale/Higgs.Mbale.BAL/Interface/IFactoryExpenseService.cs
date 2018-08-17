using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IFactoryExpenseService
    {
        IEnumerable<FactoryExpense> GetAllFactoryExpenses();
        FactoryExpense GetFactoryExpense(long factoryExpenseId);
        long SaveFactoryExpense(FactoryExpense factoryExpense, string userId);
        void MarkAsDeleted(long factoryExpenseId, string userId);
        IEnumerable<FactoryExpense> GetAllFactoryExpensesForAParticularBatch(long batchId);
            
    }
}
