using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
  public  interface ICasualActivityService
    {
        IEnumerable<CasualActivity> GetAllCasualActivities();
        CasualActivity GetCasualActivity(long casualActivityId);
        IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularCasualWorker(long casualWorkerId);
        long SaveCasualActivity(CasualActivity casualActivity, string userId);
        void MarkAsDeleted(long CasualActivityId, string userId);
        IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularBranch(long branchId);
      
    }
}
