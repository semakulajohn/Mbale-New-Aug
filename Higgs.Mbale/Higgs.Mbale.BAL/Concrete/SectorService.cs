using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;
using log4net;

namespace Higgs.Mbale.BAL.Concrete
{
  public  class SectorService : ISectorService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(SectorService));
        private ISectorDataService _dataService;
        private IUserService _userService;
        

        public SectorService(ISectorDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SectorId"></param>
        /// <returns></returns>
        public Sector GetSector(long sectorId)
        {
            var result = this._dataService.GetSector(sectorId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Sector> GetAllSectors()
        {
            var results = this._dataService.GetAllSectors();
            return MapEFToModel(results);
        } 

       
        public long SaveSector(Sector sector, string userId)
        {
            var sectorDTO = new DTO.SectorDTO()
            {
                SectorId = sector.SectorId,
                Name = sector.Name,
                Deleted = sector.Deleted,
                CreatedBy = sector.CreatedBy,
                CreatedOn = sector.CreatedOn,

            };

           var sectorId = this._dataService.SaveSector(sectorDTO, userId);

           return sectorId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SectorId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long sectorId, string userId)
        {
            _dataService.MarkAsDeleted(sectorId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Sector> MapEFToModel(IEnumerable<EF.Models.Sector> data)
        {
            var list = new List<Sector>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Sector EF object to Sector Model Object and
        /// returns the Sector model object.
        /// </summary>
        /// <param name="result">EF Sector object to be mapped.</param>
        /// <returns>Sector Model Object.</returns>
        public Sector MapEFToModel(EF.Models.Sector data)
        {
          
            var sector = new Sector()
            {
                SectorId = data.SectorId,
                Name = data.Name,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               

            };
            return sector;
        }



       #endregion
    }
}
