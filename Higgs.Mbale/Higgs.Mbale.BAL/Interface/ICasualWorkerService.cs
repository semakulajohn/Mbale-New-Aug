using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
  public  interface ICasualWorkerService
    {
        IEnumerable<CasualWorker> GetAllCasualWorkers();
        CasualWorker GetCasualWorker(long casualWorkerId);
        long SaveCasualWorker(CasualWorker casualWorker, string userId);
        void MarkAsDeleted(long casualWorkerId, string userId);
        IEnumerable<CasualWorker> GetAllCasualWorkersForAParticularBranch(long branchId);
    }
}
