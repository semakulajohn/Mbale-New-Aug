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
 public   class CasualWorkerDataService : DataServiceBase,ICasualWorkerDataService
    {
     ILog logger = log4net.LogManager.GetLogger(typeof(CasualWorkerDataService));

       public CasualWorkerDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<CasualWorker> GetAllCasualWorkers()
        {
            return this.UnitOfWork.Get<CasualWorker>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public CasualWorker GetCasualWorker(long CasualWorkerId)
        {
            return this.UnitOfWork.Get<CasualWorker>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.CasualWorkerId == CasualWorkerId &&
                    c.Deleted == false
                );
        }
        public IEnumerable<CasualWorker> GetAllCasualWorkersForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<CasualWorker>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }

        /// <summary>
        /// Saves a new CasualWorker or updates an already existing CasualWorker.
        /// </summary>
        /// <param name="CasualWorker">CasualWorker to be saved or updated.</param>
        /// <param name="CasualWorkerId">CasualWorkerId of the CasualWorker creating or updating</param>
        /// <returns>CasualWorkerId</returns>
        public long SaveCasualWorker(CasualWorkerDTO casualWorkerDTO, string userId)
        {
            long casualWorkerId = 0;
            
            if (casualWorkerDTO.CasualWorkerId == 0)
            {
           
                var casualWorker = new CasualWorker()
                {
                      
                     FirstName  = casualWorkerDTO.FirstName,
                     LastName = casualWorkerDTO.LastName,
                     BranchId = casualWorkerDTO.BranchId,
                     Address = casualWorkerDTO.Address,
                     PhoneNumber = casualWorkerDTO.PhoneNumber,
                     NextOfKeen = casualWorkerDTO.NextOfKeen,
                     NINNumber = casualWorkerDTO.NINNumber,
                     EmailAddress = casualWorkerDTO.EmailAddress,
                     UniqueNumber = casualWorkerDTO.UniqueNumber,
                     CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<CasualWorker>().AddNew(casualWorker);
                this.UnitOfWork.SaveChanges();
                casualWorkerId = casualWorker.CasualWorkerId;
                return casualWorkerId;
            }

            else
            {
                var result = this.UnitOfWork.Get<CasualWorker>().AsQueryable()
                    .FirstOrDefault(e => e.CasualWorkerId == casualWorkerDTO.CasualWorkerId);
                if (result != null)
                {
                    result.FirstName = casualWorkerDTO.FirstName;
                    result.LastName = casualWorkerDTO.LastName;
                    result.BranchId = casualWorkerDTO.BranchId;
                    result.Address = casualWorkerDTO.Address;
                    result.EmailAddress = casualWorkerDTO.EmailAddress;
                    result.NextOfKeen = casualWorkerDTO.NextOfKeen;
                    result.NINNumber = casualWorkerDTO.NINNumber;
                    result.UniqueNumber = casualWorkerDTO.UniqueNumber;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = casualWorkerDTO.Deleted;
                    result.DeletedBy = casualWorkerDTO.DeletedBy;
                    result.DeletedOn = casualWorkerDTO.DeletedOn;

                    this.UnitOfWork.Get<CasualWorker>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return casualWorkerDTO.CasualWorkerId;
            }
            return casualWorkerId;
        }

        public void MarkAsDeleted(long casualWorkerId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_Estate_And_RelatedData_AsDeleted(CasualWorkerId, userId);
            }

        }
    }
}
