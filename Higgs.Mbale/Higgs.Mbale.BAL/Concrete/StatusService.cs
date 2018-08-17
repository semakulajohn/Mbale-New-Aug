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
    public class StatusService: IStatusService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(StatusService));
        private IStatusDataService _dataService;
        private IUserService _userService;
        

        public StatusService(IStatusDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Status> GetAllStatuses()
        {
            var results = this._dataService.GetAllStatuses();
            return MapEFToModel(results);
        }

        #region Mapping Methods

        private IEnumerable<Status> MapEFToModel(IEnumerable<EF.Models.Status> data)
        {
            var list = new List<Status>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Status EF object to Status Model Object and
        /// returns the Status model object.
        /// </summary>
        /// <param name="result">EF Status object to be mapped.</param>
        /// <returns>Status Model Object.</returns>
        public Status MapEFToModel(EF.Models.Status data)
        {

            var status = new Status()
            {
                StatusId = data.StatusId,
                Name = data.Name,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),

            };
            return status;
        }



        #endregion

    }
}
