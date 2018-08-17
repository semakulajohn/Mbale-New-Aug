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
    public class StatusApiController : ApiController
    {
            private IStatusService _statusService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(StatusApiController));
            private string userId = string.Empty;

            public StatusApiController()
            {
            }

            public StatusApiController(IStatusService statusService,IUserService userService)
            {
                this._statusService = statusService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetAllStatuses")]
            public IEnumerable<Status> GetAllStatuses()
            {
                return _statusService.GetAllStatuses();
            }
    }
}
