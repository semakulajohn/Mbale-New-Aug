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
    public class CasualWorkerApiController : ApiController
    {
        private ICasualWorkerService _casualWorkerService;
        private IUserService _userService;
        ILog logger = log4net.LogManager.GetLogger(typeof(CasualWorkerApiController));
        private string userId = string.Empty;

        public CasualWorkerApiController()
        {
        }

        public CasualWorkerApiController(ICasualWorkerService casualWorkerService, IUserService userService)
        {
            this._casualWorkerService = casualWorkerService;
            this._userService = userService;
            userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
        }

        [HttpGet]
        [ActionName("GetCasualWorker")]
        public CasualWorker GetCasualWorker(long casualWorkerId)
        {
            return _casualWorkerService.GetCasualWorker(casualWorkerId);
        }

        [HttpGet]
        [ActionName("GetAllCasualWorkers")]
        public IEnumerable<CasualWorker> GetAllCasualWorkers()
        {
            return _casualWorkerService.GetAllCasualWorkers();
        }

        [HttpGet]
        [ActionName("GetAllCasualWorkersForAParticularBranch")]
        public IEnumerable<CasualWorker> GetAllCasualWorkersForAParticularBranch(long branchId)
        {
            return _casualWorkerService.GetAllCasualWorkersForAParticularBranch(branchId);
        }

        [HttpGet]
        [ActionName("Delete")]
        public void DeleteCasualWorker(long casualWorkerId)
        {
            _casualWorkerService.MarkAsDeleted(casualWorkerId, userId);
        }



        [HttpPost]
        [ActionName("Save")]
        public long Save(CasualWorker model)
        {

            var casualWorkerId = _casualWorkerService.SaveCasualWorker(model, userId);
            return casualWorkerId;
        }
    }
}
