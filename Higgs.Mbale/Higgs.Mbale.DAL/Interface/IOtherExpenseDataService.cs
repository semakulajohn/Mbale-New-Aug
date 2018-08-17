using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface IOtherExpenseDataService
    {
        IEnumerable<OtherExpense> GetAllOtherExpenses();
        OtherExpense GetOtherExpense(long otherExpenseId);
        long SaveOtherExpense(OtherExpenseDTO otherExpense, string userId);
        void MarkAsDeleted(long otherExpenseId, string userId);
        IEnumerable<OtherExpense> GetAllOtherExpensesForAParticularBatch(long batchId);
    }
}
