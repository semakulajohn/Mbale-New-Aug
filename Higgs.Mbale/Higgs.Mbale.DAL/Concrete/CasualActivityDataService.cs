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
  public  class CasualActivityDataService : DataServiceBase,ICasualActivityDataService
    {
        
     ILog logger = log4net.LogManager.GetLogger(typeof(CasualActivityDataService));

       public CasualActivityDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<CasualActivity> GetAllCasualActivities()
        {
            return this.UnitOfWork.Get<CasualActivity>().AsQueryable().Where(e => e.Deleted == false); 
        }


        public IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularCasualWorker(long casualWorkerId)
        {
            return this.UnitOfWork.Get<CasualActivity>().AsQueryable().Where(e => e.Deleted == false && e.CasualWorkerId == casualWorkerId);
        }

        public IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<CasualActivity>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }
        public CasualActivity GetCasualActivity(long casualActivityId)
        {
            return this.UnitOfWork.Get<CasualActivity>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.CasualActivityId == casualActivityId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new CasualActivity or updates an already existing CasualActivity.
        /// </summary>
        /// <param name="CasualActivity">CasualActivity to be saved or updated.</param>
        /// <param name="CasualActivityId">CasualActivityId of the CasualActivity creating or updating</param>
        /// <returns>CasualActivityId</returns>
        public long SaveCasualActivity(CasualActivityDTO casualActivityDTO, string userId)
        {
            long casualActivityId = 0;
            
            if (casualActivityDTO.CasualActivityId == 0)
            {
           
                var casualActivity = new CasualActivity()
                {
                     Quantity = casualActivityDTO.Quantity,
                     Amount= casualActivityDTO.Amount,
                     CasualWorkerId = casualActivityDTO.CasualWorkerId,
                    ActivityId = casualActivityDTO.ActivityId,
                    BranchId = casualActivityDTO.BranchId,
                    Notes = casualActivityDTO.Notes,
                    SectorId = casualActivityDTO.SectorId,
                    BatchId = casualActivityDTO.BatchId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
                     

                };

                this.UnitOfWork.Get<CasualActivity>().AddNew(casualActivity);
                this.UnitOfWork.SaveChanges();
                casualActivityId = casualActivity.CasualActivityId;
                return casualActivityId;
            }

            else
            {
                var result = this.UnitOfWork.Get<CasualActivity>().AsQueryable()
                    .FirstOrDefault(e => e.CasualActivityId == casualActivityDTO.CasualActivityId);
                if (result != null)
                {
                    result.Amount = casualActivityDTO.Amount;
                    result.UpdatedBy = userId;
                    result.ActivityId = casualActivityDTO.ActivityId;
                    result.CasualWorkerId = casualActivityDTO.CasualWorkerId;
                    result.BatchId = casualActivityDTO.BatchId;
                    result.Notes = casualActivityDTO.Notes;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = casualActivityDTO.Deleted;
                    result.BranchId = casualActivityDTO.BranchId;
                    result.SectorId = casualActivityDTO.SectorId;
                    result.DeletedBy = casualActivityDTO.DeletedBy;
                    result.DeletedOn = casualActivityDTO.DeletedOn;
                    result.Quantity = casualActivityDTO.Quantity;

                    this.UnitOfWork.Get<CasualActivity>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return casualActivityDTO.CasualActivityId;
            }
            return casualActivityId;
        }

        public void MarkAsDeleted(long casualActivityId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
                dbContext.Mark_CasualActivity_AsDeleted(casualActivityId, userId);
               
            }

        }

        public void SaveActivityBatchCasual(ActivityBatchCasualDTO activityBatchCasualDTO, string userId)
        {
            


                var activityBatchCasual = new ActivityBatchCasual()
                {
                    CasualWorkerId = activityBatchCasualDTO.CasualWorkerId,
                    BatchId = activityBatchCasualDTO.BatchId,
                    ActivityId = activityBatchCasualDTO.ActivityId,

                    Amount = activityBatchCasualDTO.Amount,
                    CreatedBy = userId,
                   Timestamp = DateTime.Now,
                   Deleted = false,


                };

                this.UnitOfWork.Get<ActivityBatchCasual>().AddNew(activityBatchCasual);
                this.UnitOfWork.SaveChanges();
                
               
            
        }

    }
}
