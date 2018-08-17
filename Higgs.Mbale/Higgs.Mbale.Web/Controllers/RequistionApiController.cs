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
    public class RequistionApiController : ApiController
    {
         private IRequistionService _requistionService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(RequistionApiController));
            private string userId = string.Empty;

            public RequistionApiController()
            {
            }

            public RequistionApiController(IRequistionService requistionService,IUserService userService)
            {
                this._requistionService = requistionService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetRequistion")]
            public Requistion GetRequistion(long requistionId)
            {
                return _requistionService.GetRequistion(requistionId);
            }

            [HttpGet]
            [ActionName("GetAllRequistions")]
            public IEnumerable<Requistion> GetAllRequistions()
            {
                return _requistionService.GetAllRequistions();
            }
            [HttpGet]
            [ActionName("GetAllRequistionsForAParticularStatus")]
            public IEnumerable<Requistion> GetAllRequistionsForAParticularStatus(long statusId)
            {
                return _requistionService.GetAllRequistionsForAParticularStatus(statusId);
            }

            [HttpGet]
            [ActionName("GetAllRequistionsForAParticularBranch")]
            public IEnumerable<Requistion> GetAllRequistionsForAParticularBranch(long branchId)
            {
                return _requistionService.GetAllRequistionsForAParticularBranch(branchId);
            }
            [HttpGet]
            [ActionName("Delete")]
            public void DeleteRequistion(long requistionId)
            {
                _requistionService.MarkAsDeleted(requistionId, userId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(Requistion model)
            {
                var requistionId = _requistionService.SaveRequistion(model, userId);
                return requistionId;
            }
    }
}
