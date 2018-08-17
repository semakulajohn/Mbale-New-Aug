using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
  public  interface IDebtorService
    {
        IEnumerable<Debtor> GetAllDebtors();
        Debtor GetDebtor(long debtorId);
        long SaveDebtor(Debtor debtor, string userId);
        void MarkAsDeleted(long debtorId, string userId);
        IEnumerable<Debtor> GetAllDebtorsForAParticularBranch(long branchId);
    }
}
