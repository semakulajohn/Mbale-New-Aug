using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface ICasualWorkerDataService
    {
        IEnumerable<CasualWorker> GetAllCasualWorkers();
        CasualWorker GetCasualWorker(long casualWorkerId);
        long SaveCasualWorker(CasualWorkerDTO casualWorker, string userId);
        void MarkAsDeleted(long casualWorkerId, string userId);
        IEnumerable<CasualWorker> GetAllCasualWorkersForAParticularBranch(long branchId);
    }
}
