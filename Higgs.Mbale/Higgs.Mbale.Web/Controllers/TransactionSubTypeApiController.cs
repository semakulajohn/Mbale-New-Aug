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
    public class TransactionSubTypeApiController : ApiController
    {
        private ITransactionSubTypeService _transactionSubTypeService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(TransactionSubTypeApiController));
            private string userId = string.Empty;

            public TransactionSubTypeApiController()
            {
            }

            public TransactionSubTypeApiController(ITransactionSubTypeService transactionSubTypeService,IUserService userService)
            {
                this._transactionSubTypeService = transactionSubTypeService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetTransactionSubType")]
            public TransactionSubType GetTransactionSubType(long transactionSubTypeId)
            {
                return _transactionSubTypeService.GetTransactionSubType(transactionSubTypeId);
            }

            [HttpGet]
            [ActionName("GetAllTransactionSubTypes")]
            public IEnumerable<TransactionSubType> GetAllTransactionSubTypes()
            {
                return _transactionSubTypeService.GetAllTransactionSubTypes();
            }

            [HttpGet]
            [ActionName("GetAllTransactionTypes")]
            public IEnumerable<TransactionType> GetAllTransactionTypes()
            {
                return _transactionSubTypeService.GetAllTransactionTypes();
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteTransactionSubType(long transactionSubTypeId)
            {
                _transactionSubTypeService.MarkAsDeleted(transactionSubTypeId, userId);
            }

         

            [HttpPost]
            [ActionName("Save")]
            public long Save(TransactionSubType model)
            {

                var transactionSubTypeId = _transactionSubTypeService.SaveTransactionSubType(model, userId);
                return transactionSubTypeId;
            }
    }
}
