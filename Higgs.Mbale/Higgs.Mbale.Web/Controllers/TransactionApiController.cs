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
    public class TransactionApiController : ApiController
    {
         private ITransactionService _transactionService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(TransactionApiController));
            private string userId = string.Empty;

            public TransactionApiController()
            {
            }

            public TransactionApiController(ITransactionService transactionService,IUserService userService)
            {
                this._transactionService = transactionService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

          

            [HttpGet]
            [ActionName("GetAllTransactions")]
            public IEnumerable<Transaction> GetAllTransactions()
            {
                return _transactionService.GetAllTransactions();
            }

            [HttpGet]
            [ActionName("GetAllTransactionsForAParticularTransactionType")]
            public IEnumerable<Transaction> GetAllTransactionsForAParticularTransactionType(long transactionTypeId)
            {
                return _transactionService.GetAllTransactionsForAParticularTransactionType(transactionTypeId);
            }

            [HttpGet]
            [ActionName("GetAllTransactionsForAParticularBranch")]
            public IEnumerable<Transaction> GetAllTransactionsForAParticularBranch(long branchId)
            {
                return _transactionService.GetAllTransactionsForAParticularBranch(branchId);
            }

            [HttpGet]
            [ActionName("GetTransaction")]
            public Transaction GetTransaction(long transactionId)
            {
                return _transactionService.GetTransaction(transactionId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(Transaction model)
            {
                var transactionId = _transactionService.SaveTransaction(model, userId);
                return transactionId;
            }
    }
}
