using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IActivityService
    {
        IEnumerable<Activity> GetAllActivities();
        Activity GetActivity(long activityId);
        long SaveActivity(Activity activity, string userId);
        void MarkAsDeleted(long activityId, string userId);
    }
}
