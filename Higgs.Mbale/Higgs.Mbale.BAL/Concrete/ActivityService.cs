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
public   class ActivityService : IActivityService
    {
    
       ILog logger = log4net.LogManager.GetLogger(typeof(ActivityService));
        private IActivityDataService _dataService;
        private IUserService _userService;
        

        public ActivityService(IActivityDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <returns></returns>
        public Activity GetActivity(long activityId)
        {
            var result = this._dataService.GetActivity(activityId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Activity> GetAllActivities()
        {
            var results = this._dataService.GetAllActivities();
            return MapEFToModel(results);
        } 

       
        public long SaveActivity(Activity activity, string userId)
        {
            var activityDTO = new DTO.ActivityDTO()
            {
                ActivityId = activity.ActivityId,
                Name = activity.Name,
                Charge = activity.Charge,
                Deleted = activity.Deleted,
                CreatedBy = activity.CreatedBy,
                CreatedOn = activity.CreatedOn,

            };

           var activityId = this._dataService.SaveActivity(activityDTO, userId);

           return activityId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long activityId, string userId)
        {
            _dataService.MarkAsDeleted(activityId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Activity> MapEFToModel(IEnumerable<EF.Models.Activity> data)
        {
            var list = new List<Activity>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Activity EF object to Activity Model Object and
        /// returns the Activity model object.
        /// </summary>
        /// <param name="result">EF Activity object to be mapped.</param>
        /// <returns>Activity Model Object.</returns>
        public Activity MapEFToModel(EF.Models.Activity data)
        {
          
            var activity = new Activity()
            {
                ActivityId = data.ActivityId,
                Name = data.Name,
                Charge = data.Charge,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),

            };
            return activity;
        }



       #endregion
    }
}
