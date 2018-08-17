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
    public class GradeApiController : ApiController
    {
          private IGradeService _gradeService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(GradeApiController));
            private string userId = string.Empty;

            public GradeApiController()
            {
            }

            public GradeApiController(IGradeService gradeService,IUserService userService)
            {
                this._gradeService = gradeService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetAllGrades")]
            public IEnumerable<Grade> GetAllGrades()
            {
                return _gradeService.GetAllGrades();
            }
    }
}
