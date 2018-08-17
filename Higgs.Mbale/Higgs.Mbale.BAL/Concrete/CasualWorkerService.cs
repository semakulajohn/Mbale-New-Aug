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
  public  class CasualWorkerService : ICasualWorkerService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(CasualWorkerService));
        private ICasualWorkerDataService _dataService;
        private IUserService _userService;
        

        public CasualWorkerService(ICasualWorkerDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CasualWorkerId"></param>
        /// <returns></returns>
        public CasualWorker GetCasualWorker(long casualWorkerId)
        {
            var result = this._dataService.GetCasualWorker(casualWorkerId);
            return MapEFToModel(result);
        }

        public IEnumerable<CasualWorker> GetAllCasualWorkersForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllCasualWorkersForAParticularBranch(branchId);
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CasualWorker> GetAllCasualWorkers()
        {
            var results = this._dataService.GetAllCasualWorkers();
            return MapEFToModel(results);
        } 

       
        public long SaveCasualWorker(CasualWorker casualWorker, string userId)
        {
            var casualWorkerDTO = new DTO.CasualWorkerDTO()
            {
                CasualWorkerId = casualWorker.CasualWorkerId,
                FirstName = casualWorker.FirstName,
                LastName = casualWorker.LastName,
                Address = casualWorker.Address,
                PhoneNumber = casualWorker.PhoneNumber,
                BranchId = casualWorker.BranchId,
                EmailAddress = casualWorker.EmailAddress,
                NINNumber = casualWorker.NINNumber,
                UniqueNumber = casualWorker.UniqueNumber,
                NextOfKeen = casualWorker.NextOfKeen,
                Deleted = casualWorker.Deleted,
                CreatedBy = casualWorker.CreatedBy,
                CreatedOn = casualWorker.CreatedOn,

            };

           var casualWorkerId = this._dataService.SaveCasualWorker(casualWorkerDTO, userId);

           return casualWorkerId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CasualWorkerId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long casualWorkerId, string userId)
        {
            _dataService.MarkAsDeleted(casualWorkerId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<CasualWorker> MapEFToModel(IEnumerable<EF.Models.CasualWorker> data)
        {
            var list = new List<CasualWorker>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps CasualWorker EF object to CasualWorker Model Object and
        /// returns the CasualWorker model object.
        /// </summary>
        /// <param name="result">EF CasualWorker object to be mapped.</param>
        /// <returns>CasualWorker Model Object.</returns>
        public CasualWorker MapEFToModel(EF.Models.CasualWorker data)
        {
          
            var casualWorker = new CasualWorker()
            {
                CasualWorkerId = data.CasualWorkerId,
                FirstName = data.FirstName,
                BranchId = data.BranchId,
                BranchName = data.Branch != null ? data.Branch.Name : "",
                LastName = data.LastName,
                Address = data.Address,
                PhoneNumber = data.PhoneNumber,
                NINNumber = data.NINNumber,
                NextOfKeen = data.NextOfKeen,
                UniqueNumber = data.UniqueNumber,
                EmailAddress = data.EmailAddress,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               

            };
            return casualWorker;
        }



       #endregion
    }
}
