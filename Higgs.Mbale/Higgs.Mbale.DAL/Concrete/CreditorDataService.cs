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
 public   class CreditorDataService : DataServiceBase,ICreditorDataService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(CreditorDataService));

       public CreditorDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
              
        public IEnumerable<Creditor> GetAllCreditors()
        {
            return this.UnitOfWork.Get<Creditor>().AsQueryable().Where(e => e.Deleted == false && e.Action == false); 
        }

        public Creditor GetCreditor(long CreditorId)
        {
            return this.UnitOfWork.Get<Creditor>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.CreditorId == CreditorId &&
                    c.Deleted == false
                );
        }

        public IEnumerable<Creditor> GetAllCreditorRecordsForParticularAccount(string userId)
        {
            return this.UnitOfWork.Get<Creditor>().AsQueryable().Where(e => e.Deleted == false && e.AspNetUserId == userId && e.Action == false);
        }
        public IEnumerable<Creditor> GetAllCreditorsForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Creditor>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }
        /// <summary>
        /// Saves a new Creditor or updates an already existing Creditor.
        /// </summary>
        /// <param name="Creditor">Creditor to be saved or updated.</param>
        /// <param name="CreditorId">CreditorId of the Creditor creating or updating</param>
        /// <returns>CreditorId</returns>
        public long SaveCreditor(CreditorDTO creditorDTO, string userId)
        {
            long creditorId = 0;
            
            if (creditorDTO.CreditorId == 0)
            {

                var creditor = new Creditor()
                {
                    AspNetUserId = creditorDTO.AspNetUserId,
                    CasualWorkerId = creditorDTO.CasualWorkerId,
                    BranchId = creditorDTO.BranchId,
                    Amount = creditorDTO.Amount,
                    Action = creditorDTO.Action,
                    SectorId = creditorDTO.SectorId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false, 
                };

                this.UnitOfWork.Get<Creditor>().AddNew(creditor);
                this.UnitOfWork.SaveChanges();
                creditorId = creditor.CreditorId;
                return creditorId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Creditor>().AsQueryable()
                    .FirstOrDefault(e => e.CreditorId == creditorDTO.CreditorId);
                if (result != null)
                {
                    result.AspNetUserId = creditorDTO.AspNetUserId;
                    result.CasualWorkerId = creditorDTO.CasualWorkerId;
                    result.Amount = creditorDTO.Amount;
                    result.Action = creditorDTO.Action;
                    result.BranchId = creditorDTO.BranchId;
                    result.SectorId = creditorDTO.SectorId;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = creditorDTO.Deleted;
                    result.DeletedBy = creditorDTO.DeletedBy;
                    result.DeletedOn = creditorDTO.DeletedOn;

                    this.UnitOfWork.Get<Creditor>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return creditorDTO.CreditorId;
            }            
        }

        public void MarkAsDeleted(long creditorId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
              //TODO: THROW NOT IMPLEMENTED EXCEPTION
            }

        }
    }
}
