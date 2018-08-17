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
    public class BatchOutPutApiController : ApiController
    {
          private IBatchOutPutService _batchOutPutService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(BatchOutPutApiController));
            private string userId = string.Empty;

            public BatchOutPutApiController()
            {
            }

            public BatchOutPutApiController(IBatchOutPutService batchOutPutService,IUserService userService)
            {
                this._batchOutPutService = batchOutPutService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetBatchOutPut")]
            public BatchOutPut GetBatchOutPut(long batchOutPutId)
            {
                return _batchOutPutService.GetBatchOutPut(batchOutPutId);
            }

            [HttpGet]
            [ActionName("GetAllBatchOutPuts")]
            public IEnumerable<BatchOutPut> GetAllBatchOutPuts()
            {
                return _batchOutPutService.GetAllBatchOutPuts();
            }

            [HttpGet]
            [ActionName("GetAllBatchOutPutsForAParticularBatch")]
            public IEnumerable<BatchOutPut> GetAllBatchOutPutsForAParticularBatch(long batchId)
            {
                return _batchOutPutService.GetAllBatchOutPutsForAParticularBatch(batchId);
            }


            [HttpGet]
            [ActionName("Delete")]
            public void DeleteBatchOutPut(long batchOutPutId)
            {
                _batchOutPutService.MarkAsDeleted(batchOutPutId, userId);
            }

         

            [HttpPost]
            [ActionName("Save")]
            public long Save(BatchOutPut model)
            {

                var batchOutPutId = _batchOutPutService.SaveBatchOutPut(model, userId);
                return batchOutPutId;
            }
    }
}
