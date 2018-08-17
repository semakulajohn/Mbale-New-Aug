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
    public class CustomerApiController : ApiController
    {
      
        private IUserService _userService;
       
        ILog logger = log4net.LogManager.GetLogger(typeof(CustomerApiController));
        private string userId = string.Empty;

        public CustomerApiController()
        {
        }

        public CustomerApiController(IUserService userService)
        {
            this._userService = userService;
            
            userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
        }

        [HttpGet]
        [ActionName("GetAllCustomers")]
        public IEnumerable<AspNetUser> GetAllCustomers()
        {
            return _userService.GetAllCustomers();
        }

       

       
       
    }
}
