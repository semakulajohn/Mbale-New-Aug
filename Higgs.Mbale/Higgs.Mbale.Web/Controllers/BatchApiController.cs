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
    public class BatchApiController : ApiController
    {
         private IBatchService _batchService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(BatchApiController));
            private string userId = string.Empty;

            public BatchApiController()
            {
            }

            public BatchApiController(IBatchService batchService,IUserService userService)
            {
                this._batchService = batchService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetBatch")]
            public Batch GetBatch(long batchId)
            {
                return _batchService.GetBatch(batchId);
            }

            [HttpGet]
            [ActionName("GetAllBatches")]
            public IEnumerable<Batch> GetAllBatches()
            {
                return _batchService.GetAllBatches();
            }

            [HttpGet]
            [ActionName("GetAllBatchesForAParticularBranch")]
            public IEnumerable<Batch> GetAllBatchesForAParticularBranch(long branchId)
            {
                return _batchService.GetAllBatchesForAParticularBranch(branchId);
            }


            [HttpGet]
            [ActionName("Delete")]
            public void DeleteBatch(long batchId)
            {
                _batchService.MarkAsDeleted(batchId, userId);
            }

         

            [HttpPost]
            [ActionName("Save")]
            public long Save(Batch model)
            {

                var batchId = _batchService.SaveBatch(model, userId);
                return batchId;
            }
    }
}
