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
  public  class OtherExpenseService : IOtherExpenseService
    {
          ILog logger = log4net.LogManager.GetLogger(typeof(OtherExpenseService));
        private IOtherExpenseDataService _dataService;
        private IUserService _userService;
        

        public OtherExpenseService(IOtherExpenseDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OtherExpenseId"></param>
        /// <returns></returns>
        public OtherExpense GetOtherExpense(long otherExpenseId)
        {
            var result = this._dataService.GetOtherExpense(otherExpenseId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OtherExpense> GetAllOtherExpenses()
        {
            var results = this._dataService.GetAllOtherExpenses();
            return MapEFToModel(results);
        }

        public IEnumerable<OtherExpense> GetAllOtherExpensesForAParticularBatch(long batchId)
        {
            var results = this._dataService.GetAllOtherExpensesForAParticularBatch(batchId);
            return MapEFToModel(results);
        }
       
        public long SaveOtherExpense(OtherExpense otherExpense, string userId)
        {
            var otherExpenseDTO = new DTO.OtherExpenseDTO()
            {
                OtherExpenseId = otherExpense.OtherExpenseId,
                BatchId = otherExpense.BatchId,
                Description = otherExpense.Description,
                BranchId = otherExpense.BranchId,
                Amount = otherExpense.Amount,
                 Deleted = otherExpense.Deleted,
                CreatedBy = otherExpense.CreatedBy,
                CreatedOn = otherExpense.CreatedOn,
                SectorId = otherExpense.SectorId,

            };

           var OtherExpenseId = this._dataService.SaveOtherExpense(otherExpenseDTO, userId);

           return OtherExpenseId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OtherExpenseId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long otherExpenseId, string userId)
        {
            _dataService.MarkAsDeleted(otherExpenseId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<OtherExpense> MapEFToModel(IEnumerable<EF.Models.OtherExpense> data)
        {
            var list = new List<OtherExpense>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps OtherExpense EF object to OtherExpense Model Object and
        /// returns the OtherExpense model object.
        /// </summary>
        /// <param name="result">EF OtherExpense object to be mapped.</param>
        /// <returns>OtherExpense Model Object.</returns>
        public OtherExpense MapEFToModel(EF.Models.OtherExpense data)
        {
          
            var otherExpense = new OtherExpense()
            {
                OtherExpenseId = data.OtherExpenseId,
                Amount = data.Amount,
                SectorId = data.SectorId,
                BranchId = data.BranchId,
                BatchId = data.BatchId,
                Description = data.Description,
                BranchName = data.Branch != null ? data.Branch.Name : "",
                SectorName = data.Sector != null ? data.Sector.Name : "",
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                 Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               

            };
            return otherExpense;
        }



       #endregion
    }
}
