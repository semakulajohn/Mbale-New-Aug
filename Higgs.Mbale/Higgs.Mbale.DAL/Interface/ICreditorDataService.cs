using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface ICreditorDataService
    {
        IEnumerable<Creditor> GetAllCreditors();
        Creditor GetCreditor(long creditorId);
        long SaveCreditor(CreditorDTO creditor, string userId);
        void MarkAsDeleted(long creditorId, string userId);
        IEnumerable<Creditor> GetAllCreditorsForAParticularBranch(long branchId);
        IEnumerable<Creditor> GetAllCreditorRecordsForParticularAccount(string userId);
    }
}
