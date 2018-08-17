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
    public class CreditorApiController : ApiController
    {
         private ICreditorService _creditorService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(CreditorApiController));
            private string userId = string.Empty;

            public CreditorApiController()
            {
            }

            public CreditorApiController(ICreditorService creditorService,IUserService userService)
            {
                this._creditorService = creditorService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

          

            [HttpGet]
            [ActionName("GetAllCreditors")]
            public IEnumerable<Creditor> GetAllCreditors()
            {
                return _creditorService.GetAllCreditors();
            }


            [HttpGet]
            [ActionName("GetAllBranchCreditors")]
            public IEnumerable<Creditor> GetAllBranchCreditors(long branchId)
            {
                return _creditorService.GetAllCreditorsForAParticularBranch(branchId);
            }
          
    }
}
