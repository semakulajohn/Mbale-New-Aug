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
    public class DebtorApiController : ApiController
    {
         private IDebtorService _debtorService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(DebtorApiController));
            private string userId = string.Empty;

            public DebtorApiController()
            {
            }

            public DebtorApiController(IDebtorService debtorService,IUserService userService)
            {
                this._debtorService = debtorService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            

            [HttpGet]
            [ActionName("GetAllDebtors")]
            public IEnumerable<Debtor> GetAllDebtors()
            {
                return _debtorService.GetAllDebtors();
            }

            [HttpGet]
            [ActionName("GetAllBranchDebtors")]
            public IEnumerable<Debtor> GetAllBranchDebtors(long branchId)
            {
                return _debtorService.GetAllDebtorsForAParticularBranch(branchId);
            }

           
    }
}
