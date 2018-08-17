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
 public   class StoreDataService : DataServiceBase,IStoreDataService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(StoreDataService));

       public StoreDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Store> GetAllStores()
        {
            return this.UnitOfWork.Get<Store>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public Store GetStore(long storeId)
        {
            return this.UnitOfWork.Get<Store>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.StoreId == storeId &&
                    c.Deleted == false
                );
        }
        public IEnumerable<Store> GetAllStoresForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Store>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }
        /// <summary>
        /// Saves a new Store or updates an already existing Store.
        /// </summary>
        /// <param name="Store">Store to be saved or updated.</param>
        /// <param name="StoreId">StoreId of the Store creating or updating</param>
        /// <returns>StoreId</returns>
        public long SaveStore(StoreDTO storeDTO, string userId)
        {
            long storeId = 0;
            
            if (storeDTO.StoreId == 0)
            {
           
                var store = new Store()
                {
                    Name = storeDTO.Name,
                    BranchId = storeDTO.BranchId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<Store>().AddNew(store);
                this.UnitOfWork.SaveChanges();
                storeId = store.StoreId;
                return storeId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Store>().AsQueryable()
                    .FirstOrDefault(e => e.StoreId == storeDTO.StoreId);
                if (result != null)
                {
                    result.Name = storeDTO.Name;
                    result.UpdatedBy = userId;
                    result.BranchId = storeDTO.BranchId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = storeDTO.Deleted;
                    result.DeletedBy = storeDTO.DeletedBy;
                    result.DeletedOn = storeDTO.DeletedOn;

                    this.UnitOfWork.Get<Store>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return storeDTO.StoreId;
            }
            return storeId;
        }

        public void MarkAsDeleted(long storeId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_store_And_RelatedData_AsDeleted(StoreId, userId);
            }

        }
    }
}
