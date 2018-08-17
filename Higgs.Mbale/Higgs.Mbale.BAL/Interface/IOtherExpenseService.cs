using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IOtherExpenseService
    {
        IEnumerable<OtherExpense> GetAllOtherExpenses();
        OtherExpense GetOtherExpense(long otherExpenseId);
        long SaveOtherExpense(OtherExpense otherExpense, string userId);
        void MarkAsDeleted(long otherExpenseId, string userId);
        IEnumerable<OtherExpense> GetAllOtherExpensesForAParticularBatch(long batchId);
            
    }
}
