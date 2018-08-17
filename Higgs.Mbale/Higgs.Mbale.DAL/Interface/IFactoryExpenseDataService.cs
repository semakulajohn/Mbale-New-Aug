using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface IFactoryExpenseDataService
    {
        IEnumerable<FactoryExpense> GetAllFactoryExpenses();
        FactoryExpense GetFactoryExpense(long factoryExpenseId);
        long SaveFactoryExpense(FactoryExpenseDTO factoryExpense, string userId);
        void MarkAsDeleted(long factoryExpenseId, string userId);
        IEnumerable<FactoryExpense> GetAllFactoryExpensesForAParticularBatch(long batchId);
    }
}
