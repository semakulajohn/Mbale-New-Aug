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
   public class DebtorDataService : DataServiceBase,IDebtorDataService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(DebtorDataService));

       public DebtorDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
              
        public IEnumerable<Debtor> GetAllDebtors()
        {
            return this.UnitOfWork.Get<Debtor>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public Debtor GetDebtor(long debtorId)
        {
            return this.UnitOfWork.Get<Debtor>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.DebtorId == debtorId &&
                    c.Deleted == false
                );
        }
        public IEnumerable<Debtor> GetAllDebtorsForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Debtor>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }
        /// <summary>
        /// Saves a new Debtor or updates an already existing Debtor.
        /// </summary>
        /// <param name="Debtor">Debtor to be saved or updated.</param>
        /// <param name="DebtorId">DebtorId of the Debtor creating or updating</param>
        /// <returns>DebtorId</returns>
        public long SaveDebtor(DebtorDTO debtorDTO, string userId)
        {
            long debtorId = 0;
            
            if (debtorDTO.DebtorId == 0)
            {

                var debtor = new Debtor()
                {
                    AspNetUserId = debtorDTO.AspNetUserId,
                    CasualWorkerId = debtorDTO.CasualWorkerId,
                    BranchId = debtorDTO.BranchId,
                    Amount = debtorDTO.Amount,
                    Action = debtorDTO.Action,
                    SectorId = debtorDTO.SectorId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false, 
                };

                this.UnitOfWork.Get<Debtor>().AddNew(debtor);
                this.UnitOfWork.SaveChanges();
                debtorId = debtor.DebtorId;
                return debtorId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Debtor>().AsQueryable()
                    .FirstOrDefault(e => e.DebtorId == debtorDTO.DebtorId);
                if (result != null)
                {
                    result.AspNetUserId = debtorDTO.AspNetUserId;
                    result.CasualWorkerId = debtorDTO.CasualWorkerId;
                    result.Amount = debtorDTO.Amount;
                    result.Action = debtorDTO.Action;
                    result.BranchId = debtorDTO.BranchId;
                    result.SectorId = debtorDTO.SectorId;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = debtorDTO.Deleted;
                    result.DeletedBy = debtorDTO.DeletedBy;
                    result.DeletedOn = debtorDTO.DeletedOn;

                    this.UnitOfWork.Get<Debtor>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return debtorDTO.DebtorId;
            }            
        }

        public void MarkAsDeleted(long debtorId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
              //TODO: THROW NOT IMPLEMENTED EXCEPTION
            }

        }
    }
}
