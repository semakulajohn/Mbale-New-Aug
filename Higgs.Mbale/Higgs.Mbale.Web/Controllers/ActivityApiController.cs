using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Higgs.Mbale.BAL.Interface;
using log4net;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.Web.Controllers
{
    public class ActivityApiController : ApiController
    {
        
            private IActivityService _activityService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(ActivityApiController));
            private string userId = string.Empty;

            public ActivityApiController()
            {
            }

            public ActivityApiController(IActivityService activityService,IUserService userService)
            {
                this._activityService = activityService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("Getactivity")]
            public Activity GetActivity(long activityId)
            {
                return _activityService.GetActivity(activityId);
            }

            [HttpGet]
            [ActionName("GetAllActivities")]
            public IEnumerable<Activity> GetAllActivities()
            {
                return _activityService.GetAllActivities();
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteActivity(long activityId)
            {
                _activityService.MarkAsDeleted(activityId, userId);
            }


            [HttpPost]
            [ActionName("Save")]
            public long Save(Activity model)
            {

                var activityId = _activityService.SaveActivity(model, userId);
                return activityId;
            }
    }
}
