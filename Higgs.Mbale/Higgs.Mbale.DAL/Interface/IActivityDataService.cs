using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface IActivityDataService
    {
        IEnumerable<Activity> GetAllActivities();
        Activity GetActivity(long activityId);
        long SaveActivity(ActivityDTO activity, string userId);
        void MarkAsDeleted(long activityId, string userId);
    }
}
