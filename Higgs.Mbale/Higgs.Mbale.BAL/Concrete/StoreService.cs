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
 public   class StoreService : IStoreService
    {
          ILog logger = log4net.LogManager.GetLogger(typeof(StoreService));
        private IStoreDataService _dataService;
        private IUserService _userService;
        

        public StoreService(IStoreDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StoreId"></param>
        /// <returns></returns>
        public Store GetStore(long storeId)
        {
            var result = this._dataService.GetStore(storeId);
            return MapEFToModel(result);
        }

        public IEnumerable<Store> GetAllStoresForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllStoresForAParticularBranch(branchId);
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Store> GetAllStores()
        {
            var results = this._dataService.GetAllStores();
            return MapEFToModel(results);
        }

       

        public long SaveStore(Store store, string userId)
        {
            var storeDTO = new DTO.StoreDTO()
            {
                StoreId = store.StoreId,
                Name = store.Name,
                BranchId = store.BranchId, 
                Deleted = store.Deleted,
                CreatedBy = store.CreatedBy,
                CreatedOn = store.CreatedOn,

            };

           var StoreId = this._dataService.SaveStore(storeDTO, userId);

           return StoreId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StoreId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long storeId, string userId)
        {
            _dataService.MarkAsDeleted(storeId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Store> MapEFToModel(IEnumerable<EF.Models.Store> data)
        {
            var list = new List<Store>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Store EF object to Store Model Object and
        /// returns the Store model object.
        /// </summary>
        /// <param name="result">EF Store object to be mapped.</param>
        /// <returns>Store Model Object.</returns>
        public Store MapEFToModel(EF.Models.Store data)
        {
          
            var store = new Store()
            {
                StoreId = data.StoreId,
                Name = data.Name,
                BranchId = data.BranchId,
                BranchName = data.Branch != null? data.Branch.Name : "",
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               

            };
            return store;
        }



       #endregion
    }
}
