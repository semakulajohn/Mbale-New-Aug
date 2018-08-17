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
 public   class CasualActivityService : ICasualActivityService
    {
      ILog logger = log4net.LogManager.GetLogger(typeof(CasualActivityService));
        private ICasualActivityDataService _dataService;
        private IUserService _userService;
        private IActivityService _activityService;
        private IAccountTransactionActivityService _accountTransactionActivityService;
        

        public CasualActivityService(ICasualActivityDataService dataService,IUserService userService,IActivityService activityService,
            IAccountTransactionActivityService accountTransactionActivityService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._activityService = activityService;
            this._accountTransactionActivityService = accountTransactionActivityService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CasualActivityId"></param>
        /// <returns></returns>
        public CasualActivity GetCasualActivity(long casualActivityId)
        {
            var result = this._dataService.GetCasualActivity(casualActivityId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CasualActivity> GetAllCasualActivities()
        {
            var results = this._dataService.GetAllCasualActivities();
            return MapEFToModel(results);
        }

        public IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllCasualActivitiesForAParticularBranch(branchId);
            return MapEFToModel(results);
        }

        public IEnumerable<CasualActivity> GetAllCasualActivitiesForAParticularCasualWorker(long casualWorkerId)
        {
            var results = this._dataService.GetAllCasualActivitiesForAParticularCasualWorker(casualWorkerId);
            return MapEFToModel(results);
        }
       
        public long SaveCasualActivity(CasualActivity casualActivity, string userId)
        {
          
       
            var casualActivityDTO = new DTO.CasualActivityDTO()
            {
                CasualActivityId = casualActivity.CasualActivityId,
                BatchId = casualActivity.BatchId,
                CasualWorkerId = casualActivity.CasualWorkerId,
                Quantity = casualActivity.Quantity,
                ActivityId = casualActivity.ActivityId,
                BranchId = casualActivity.BranchId,
                Amount = casualActivity.Amount,
                Notes = casualActivity.Notes,
                 Deleted = casualActivity.Deleted,
                CreatedBy = casualActivity.CreatedBy,
                CreatedOn = casualActivity.CreatedOn,
                SectorId = casualActivity.SectorId,

            };

           var casualActivityId = this._dataService.SaveCasualActivity(casualActivityDTO, userId);

           var activityBatchCasualDTO = new DTO.ActivityBatchCasualDTO()
           {

               CasualWorkerId = casualActivity.CasualWorkerId,
               BatchId = casualActivity.BatchId,
               ActivityId = casualActivity.ActivityId,
               Amount = casualActivity.Amount,
               CreatedBy = userId,


           };

           this._dataService.SaveActivityBatchCasual(activityBatchCasualDTO, userId);

           var accountActivityDTO = new AccountTransactionActivity()
           {
               
               TransactionSubTypeId = casualActivity.TransactionSubTypeId,
               Action = casualActivity.Action,
               Amount = casualActivity.Amount,
               Notes = casualActivity.Notes,
               CasualWorkerId = casualActivity.CasualWorkerId,
               BranchId = casualActivity.BranchId,
               SectorId = casualActivity.SectorId,
               Deleted = casualActivity.Deleted,
               CreatedBy = casualActivity.CreatedBy,
               CreatedOn =Convert.ToDateTime(casualActivity.CreatedOn)

           };
           var accountActivityId = _accountTransactionActivityService.SaveAccountTransactionActivity(accountActivityDTO,userId);

           return casualActivityId;
                      
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CasualActivityId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long CasualActivityId, string userId)
        {
            _dataService.MarkAsDeleted(CasualActivityId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<CasualActivity> MapEFToModel(IEnumerable<EF.Models.CasualActivity> data)
        {
            var list = new List<CasualActivity>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps CasualActivity EF object to CasualActivity Model Object and
        /// returns the CasualActivity model object.
        /// </summary>
        /// <param name="result">EF CasualActivity object to be mapped.</param>
        /// <returns>CasualActivity Model Object.</returns>
        public CasualActivity MapEFToModel(EF.Models.CasualActivity data)
        {
            string casualName = string.Empty;
            if (data.CasualWorker != null)
            {
                casualName = data.CasualWorker.FirstName + " " + data.CasualWorker.LastName;
            }
          
            var casualActivity = new CasualActivity()
            {
                CasualActivityId = data.CasualActivityId,
                Amount = data.Amount,
                SectorId = data.SectorId,
                BranchId = data.BranchId,
                BatchId = data.BatchId,
                Notes = data.Notes,
                BatchNumber = data.Batch != null? data.Batch.Name:"",
                Quantity = data.Quantity,
                ActivityId = data.ActivityId,
                ActivityName = data.Activity != null ? data.Activity.Name : "",
                CasualWorkerId = data.CasualWorkerId,
                CasualWorkerName = casualName,
                BranchName = data.Branch != null ? data.Branch.Name : "",
                SectorName = data.Sector != null ? data.Sector.Name : "",
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                 Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               

            };
            return casualActivity;
        }



       #endregion
    }
}
