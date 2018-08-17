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
public    class BranchService : IBranchService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(BranchService));
        private IBranchDataService _dataService;
        private IUserService _userService;
        

        public BranchService(IBranchDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public Branch GetBranch(long branchId)
        {
            var result = this._dataService.GetBranch(branchId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Branch> GetAllBranches()
        {
            var results = this._dataService.GetAllBranches();
            return MapEFToModel(results);
        } 

       
        public long SaveBranch(Branch branch, string userId)
        {
            var branchDTO = new DTO.BranchDTO()
            {
                BranchId = branch.BranchId,
                Name = branch.Name,
                PhoneNumber = branch.PhoneNumber,
                Location = branch.Location, 
                Deleted = branch.Deleted,
                CreatedBy = branch.CreatedBy,
                CreatedOn = branch.CreatedOn,
                MillingChargeRate = branch.MillingChargeRate,

            };

           var branchId = this._dataService.SaveBranch(branchDTO, userId);

           return branchId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long branchId, string userId)
        {
            _dataService.MarkAsDeleted(branchId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Branch> MapEFToModel(IEnumerable<EF.Models.Branch> data)
        {
            var list = new List<Branch>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Branch EF object to Branch Model Object and
        /// returns the Branch model object.
        /// </summary>
        /// <param name="result">EF Branch object to be mapped.</param>
        /// <returns>Branch Model Object.</returns>
        public Branch MapEFToModel(EF.Models.Branch data)
        {
          
            var branch = new Branch()
            {
                BranchId = data.BranchId,
                Name = data.Name,
                PhoneNumber = data.PhoneNumber,
                Location = data.Location,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                MillingChargeRate = data.MillingChargeRate,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               

            };
            return branch;
        }



       #endregion
    }
}
