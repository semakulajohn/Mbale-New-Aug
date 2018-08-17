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
  public  class FactoryExpenseService : IFactoryExpenseService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(FactoryExpenseService));
        private IFactoryExpenseDataService _dataService;
        private IUserService _userService;
        

        public FactoryExpenseService(IFactoryExpenseDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FactoryExpenseId"></param>
        /// <returns></returns>
        public FactoryExpense GetFactoryExpense(long factoryExpenseId)
        {
            var result = this._dataService.GetFactoryExpense(factoryExpenseId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FactoryExpense> GetAllFactoryExpenses()
        {
            var results = this._dataService.GetAllFactoryExpenses();
            return MapEFToModel(results);
        }

        public IEnumerable<FactoryExpense> GetAllFactoryExpensesForAParticularBatch(long batchId)
        {
            var results = this._dataService.GetAllFactoryExpensesForAParticularBatch(batchId);
            return MapEFToModel(results);
        }
       
        public long SaveFactoryExpense(FactoryExpense factoryExpense, string userId)
        {
            var factoryExpenseDTO = new DTO.FactoryExpenseDTO()
            {
                FactoryExpenseId = factoryExpense.FactoryExpenseId,
                BatchId = factoryExpense.BatchId,
                Description = factoryExpense.Description,
                BranchId = factoryExpense.BranchId,
                Amount = factoryExpense.Amount,
                 Deleted = factoryExpense.Deleted,
                CreatedBy = factoryExpense.CreatedBy,
                CreatedOn = factoryExpense.CreatedOn,
                SectorId = factoryExpense.SectorId,

            };

           var factoryExpenseId = this._dataService.SaveFactoryExpense(factoryExpenseDTO, userId);

           return factoryExpenseId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FactoryExpenseId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long factoryExpenseId, string userId)
        {
            _dataService.MarkAsDeleted(factoryExpenseId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<FactoryExpense> MapEFToModel(IEnumerable<EF.Models.FactoryExpense> data)
        {
            var list = new List<FactoryExpense>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps FactoryExpense EF object to FactoryExpense Model Object and
        /// returns the FactoryExpense model object.
        /// </summary>
        /// <param name="result">EF FactoryExpense object to be mapped.</param>
        /// <returns>FactoryExpense Model Object.</returns>
        public FactoryExpense MapEFToModel(EF.Models.FactoryExpense data)
        {
          
            var factoryExpense = new FactoryExpense()
            {
                FactoryExpenseId = data.FactoryExpenseId,
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
            return factoryExpense;
        }



       #endregion
    }
}
