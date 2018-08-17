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
    public class CasualActivityApiController : ApiController
    {
         private ICasualActivityService _casualActivityService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(CasualActivityApiController));
            private string userId = string.Empty;

            public CasualActivityApiController()
            {
            }

            public CasualActivityApiController(ICasualActivityService casualActivityService,IUserService userService)
            {
                this._casualActivityService = casualActivityService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetCasualActivity")]
            public CasualActivity GetCasualActivity(long casualActivityId)
            {
                return _casualActivityService.GetCasualActivity(casualActivityId);
            }

            [HttpGet]
            [ActionName("GetAllCasualActivities")]
            public IEnumerable<CasualActivity> GetAllCasualActivities()
            {
                return _casualActivityService.GetAllCasualActivities();
            }

            [HttpGet]
            [ActionName("GetAllCasualActivitiesForAParticularCasualWorker")]
            public IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularCasualWorker(long casualWorkerId)
            {
                return _casualActivityService.GetAllCasualActivitiesForAParticularCasualWorker(casualWorkerId);
            }

            [HttpGet]
            [ActionName("GetAllCasualActivitiesForAParticularBranch")]
            public IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularBranch(long branchId)
            {
                return _casualActivityService.GetAllCasualActivitiesForAParticularBranch(branchId);
            }
            [HttpGet]
            [ActionName("Delete")]
            public void DeleteCasualActivity(long casualActivityId)
            {
                _casualActivityService.MarkAsDeleted(casualActivityId, userId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(CasualActivity model)
            {
                var casualActivityId = _casualActivityService.SaveCasualActivity(model, userId);
                return casualActivityId;
            }
    }
}
