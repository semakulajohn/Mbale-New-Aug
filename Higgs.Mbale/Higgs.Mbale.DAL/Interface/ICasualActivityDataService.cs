using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface ICasualActivityDataService
    {
        IEnumerable<CasualActivity> GetAllCasualActivities();
        CasualActivity GetCasualActivity(long casualActivityId);
        IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularCasualWorker(long casualWorkerId);
        long SaveCasualActivity(CasualActivityDTO casualActivity, string userId);
        void MarkAsDeleted(long CasualActivityId, string userId);
        IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularBranch(long branchId);
        void SaveActivityBatchCasual(ActivityBatchCasualDTO activityBatchCasualDTO, string userId);
     
    }
}
