using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.DAL.Concrete;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.EF.UnitOfWork;
using Higgs.Mbale.DTO;
using log4net;

namespace Higgs.Mbale.DAL.Concrete
{
 public   class ActivityDataService : DataServiceBase,IActivityDataService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(ActivityDataService));

       public ActivityDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
              
        public IEnumerable<Activity> GetAllActivities()
        {
            return this.UnitOfWork.Get<Activity>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public Activity GetActivity(long activityId)
        {
            return this.UnitOfWork.Get<Activity>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.ActivityId == activityId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new Activity or updates an already existing Activity.
        /// </summary>
        /// <param name="Activity">Activity to be saved or updated.</param>
        /// <param name="ActivityId">ActivityId of the Activity creating or updating</param>
        /// <returns>ActivityId</returns>
        public long SaveActivity(ActivityDTO activityDTO, string userId)
        {
            long activityId = 0;
            
            if (activityDTO.ActivityId == 0)
            {
           
                var activity = new Activity()
                {
                    Name = activityDTO.Name,
                    Charge = activityDTO.Charge,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false, 
                };

                this.UnitOfWork.Get<Activity>().AddNew(activity);
                this.UnitOfWork.SaveChanges();
                activityId = activity.ActivityId;
                return activityId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Activity>().AsQueryable()
                    .FirstOrDefault(e => e.ActivityId == activityDTO.ActivityId);
                if (result != null)
                {
                    result.Name = activityDTO.Name;
                    result.Charge = activityDTO.Charge;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = activityDTO.Deleted;
                    result.DeletedBy = activityDTO.DeletedBy;
                    result.DeletedOn = activityDTO.DeletedOn;

                    this.UnitOfWork.Get<Activity>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return activityDTO.ActivityId;
            }            
        }

        public void MarkAsDeleted(long activityId, string userId)
        {

            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_Estate_And_RelatedData_AsDeleted(ActivityId, userId);
            }

        }
    }
}
