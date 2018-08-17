using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface IDebtorDataService
    {
        IEnumerable<Debtor> GetAllDebtors();
        Debtor GetDebtor(long debtorId);
        long SaveDebtor(DebtorDTO debtor, string userId);
        void MarkAsDeleted(long debtorId, string userId);
        IEnumerable<Debtor> GetAllDebtorsForAParticularBranch(long branchId);
    }
}
