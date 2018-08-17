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
    public class LabourCostApiController : ApiController
    {
         private ILabourCostService _labourCostService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(LabourCostApiController));
            private string userId = string.Empty;

            public LabourCostApiController()
            {
            }

            public LabourCostApiController(ILabourCostService labourCostService,IUserService userService)
            {
                this._labourCostService = labourCostService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetLabourCost")]
            public LabourCost GetLabourCost(long labourCostId)
            {
                return _labourCostService.GetLabourCost(labourCostId);
            }

            [HttpGet]
            [ActionName("GetAllLabourCosts")]
            public IEnumerable<LabourCost> GetAllLabourCosts()
            {
                return _labourCostService.GetAllLabourCosts();
            }

            [HttpGet]
            [ActionName("GetAllLabourCostsForAParticularBatch")]
            public IEnumerable<LabourCost> GetAllLabourCostsForAParticularBatch(long batchId)
            {
                return _labourCostService.GetAllLabourCostsForAParticularBatch(batchId);
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteLabourCost(long labourCostId)
            {
                _labourCostService.MarkAsDeleted(labourCostId, userId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(LabourCost model)
            {
                var labourCostId = _labourCostService.SaveLabourCost(model, userId);
                return labourCostId;
            }
    }
}
