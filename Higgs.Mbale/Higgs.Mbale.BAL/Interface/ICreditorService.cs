using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface ICreditorService
    {
        IEnumerable<Creditor> GetAllCreditors();
        Creditor GetCreditor(long creditorId);
        long SaveCreditor(Creditor creditor, string userId);
        void MarkAsDeleted(long creditorId, string userId);
        IEnumerable<Creditor> GetAllCreditorsForAParticularBranch(long branchId);
        IEnumerable<Creditor> GetAllCreditorRecordsForParticularAccount(string aspNetUserId);
    }
}
