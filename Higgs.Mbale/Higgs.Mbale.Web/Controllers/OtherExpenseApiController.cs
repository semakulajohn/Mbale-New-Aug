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
    public class OtherExpenseApiController : ApiController
    {
         private IOtherExpenseService _otherExpenseService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(OtherExpenseApiController));
            private string userId = string.Empty;

            public OtherExpenseApiController()
            {
            }

            public OtherExpenseApiController(IOtherExpenseService otherExpenseService,IUserService userService)
            {
                this._otherExpenseService = otherExpenseService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetOtherExpense")]
            public OtherExpense GetOtherExpense(long otherExpenseId)
            {
                return _otherExpenseService.GetOtherExpense(otherExpenseId);
            }

            [HttpGet]
            [ActionName("GetAllOtherExpenses")]
            public IEnumerable<OtherExpense> GetAllOtherExpenses()
            {
                return _otherExpenseService.GetAllOtherExpenses();
            }

            [HttpGet]
            [ActionName("GetAllOtherExpensesForAParticularBatch")]
            public IEnumerable<OtherExpense> GetAllOtherExpensesForAParticularBatch(long batchId)
            {
                return _otherExpenseService.GetAllOtherExpensesForAParticularBatch(batchId);
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteOtherExpense(long otherExpenseId)
            {
                _otherExpenseService.MarkAsDeleted(otherExpenseId, userId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(OtherExpense model)
            {
                var otherExpenseId = _otherExpenseService.SaveOtherExpense(model, userId);
                return otherExpenseId;
            }
    }
}
