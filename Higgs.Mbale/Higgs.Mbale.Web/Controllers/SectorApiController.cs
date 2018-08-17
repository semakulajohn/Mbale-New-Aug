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
    public class SectorApiController : ApiController
    {
        
        private ISectorService _sectorService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(SectorApiController));
            private string userId = string.Empty;

            public SectorApiController()
            {
            }

            public SectorApiController(ISectorService sectorService,IUserService userService)
            {
                this._sectorService = sectorService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetSector")]
            public Sector GetSector(long sectorId)
            {
                return _sectorService.GetSector(sectorId);
            }

            [HttpGet]
            [ActionName("GetAllSectors")]
            public IEnumerable<Sector> GetAllSectors()
            {
                return _sectorService.GetAllSectors();
            }

          

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteSector(long sectorId)
            {
                _sectorService.MarkAsDeleted(sectorId, userId);
            }

         

            [HttpPost]
            [ActionName("Save")]
            public long Save(Sector model)
            {

                var sectorId = _sectorService.SaveSector(model, userId);
                return sectorId;
            }
    }
}
