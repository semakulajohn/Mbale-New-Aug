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
 public   class TransactionSubTypeService : ITransactionSubTypeService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(TransactionSubTypeService));
        private ITransactionSubTypeDataService _dataService;
        private IUserService _userService;
        

        public TransactionSubTypeService(ITransactionSubTypeDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionSubTypeId"></param>
        /// <returns></returns>
        public TransactionSubType GetTransactionSubType(long transactionSubTypeId)
        {
            var result = this._dataService.GetTransactionSubType(transactionSubTypeId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TransactionSubType> GetAllTransactionSubTypes()
        {
            var results = this._dataService.GetAllTransactionSubTypes();
            return MapEFToModel(results);
        }

        public IEnumerable<TransactionType> GetAllTransactionTypes()
        {
            var results = this._dataService.GetAllTransactionTypes();
            return MapEFToModel(results);
        }
       
        public long SaveTransactionSubType(TransactionSubType transactionSubType, string userId)
        {
            var transactionSubTypeDTO = new DTO.TransactionSubTypeDTO()
            {
                TransactionSubTypeId = transactionSubType.TransactionSubTypeId,
                Name = transactionSubType.Name,
                TransactionTypeId = transactionSubType.TransactionTypeId,
                
                Deleted = transactionSubType.Deleted,
                CreatedBy = transactionSubType.CreatedBy,
                CreatedOn = transactionSubType.CreatedOn,

            };

           var transactionSubTypeId = this._dataService.SaveTransactionSubType(transactionSubTypeDTO, userId);

           return transactionSubTypeId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionSubTypeId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long transactionSubTypeId, string userId)
        {
            _dataService.MarkAsDeleted(transactionSubTypeId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<TransactionSubType> MapEFToModel(IEnumerable<EF.Models.TransactionSubType> data)
        {
            var list = new List<TransactionSubType>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps TransactionSubType EF object to TransactionSubType Model Object and
        /// returns the TransactionSubType model object.
        /// </summary>
        /// <param name="result">EF TransactionSubType object to be mapped.</param>
        /// <returns>TransactionSubType Model Object.</returns>
        public TransactionSubType MapEFToModel(EF.Models.TransactionSubType data)
        {
          
            var transactionSubType = new TransactionSubType()
            {
                TransactionSubTypeId = data.TransactionSubTypeId,
                Name = data.Name,
                TransactionTypeName = data.TransactionType != null ? data.TransactionType.Name : "",
                TransactionTypeId = data.TransactionTypeId,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               

            };
            return transactionSubType;
        }


        private IEnumerable<TransactionType> MapEFToModel(IEnumerable<EF.Models.TransactionType> data)
        {
            var list = new List<TransactionType>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps TransactionType EF object to TransactionType Model Object and
        /// returns the TransactionType model object.
        /// </summary>
        /// <param name="result">EF TransactionType object to be mapped.</param>
        /// <returns>TransactionType Model Object.</returns>
        public TransactionType MapEFToModel(EF.Models.TransactionType data)
        {

            var transactionType = new TransactionType()
            {
                TransactionTypeId = data.TransactionTypeId,
                Name = data.Name,
                CreatedOn = data.CreatedOn,
                


            };
            return transactionType;
        }


       #endregion
    }
}
