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
 public   class OtherExpenseDataService : DataServiceBase, IOtherExpenseDataService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(OtherExpenseDataService));

       public OtherExpenseDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<OtherExpense> GetAllOtherExpenses()
        {
            return this.UnitOfWork.Get<OtherExpense>().AsQueryable().Where(e => e.Deleted == false); 
        }


        public IEnumerable<OtherExpense> GetAllOtherExpensesForAParticularBatch(long batchId)
        {
            return this.UnitOfWork.Get<OtherExpense>().AsQueryable().Where(e => e.Deleted == false && e.BatchId == batchId);
        }

        public OtherExpense GetOtherExpense(long otherExpenseId)
        {
            return this.UnitOfWork.Get<OtherExpense>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.OtherExpenseId == otherExpenseId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new OtherExpense or updates an already existing OtherExpense.
        /// </summary>
        /// <param name="OtherExpense">OtherExpense to be saved or updated.</param>
        /// <param name="OtherExpenseId">OtherExpenseId of the OtherExpense creating or updating</param>
        /// <returns>OtherExpenseId</returns>
        public long SaveOtherExpense(OtherExpenseDTO otherExpenseDTO, string userId)
        {
            long otherExpenseId = 0;
            
            if (otherExpenseDTO.OtherExpenseId == 0)
            {
           
                var otherExpense = new OtherExpense()
                {
                     Amount= otherExpenseDTO.Amount,
                    Description = otherExpenseDTO.Description,
                    BranchId = otherExpenseDTO.BranchId,
                    SectorId = otherExpenseDTO.SectorId,
                    BatchId = otherExpenseDTO.BatchId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
                     

                };

                this.UnitOfWork.Get<OtherExpense>().AddNew(otherExpense);
                this.UnitOfWork.SaveChanges();
                otherExpenseId = otherExpense.OtherExpenseId;
                return otherExpenseId;
            }

            else
            {
                var result = this.UnitOfWork.Get<OtherExpense>().AsQueryable()
                    .FirstOrDefault(e => e.OtherExpenseId == otherExpenseDTO.OtherExpenseId);
                if (result != null)
                {
                    result.Amount = otherExpenseDTO.Amount;
                    result.UpdatedBy = userId;
                    result.Description = otherExpenseDTO.Description;
                    result.BatchId = otherExpenseDTO.BatchId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = otherExpenseDTO.Deleted;
                    result.BranchId = otherExpenseDTO.BranchId;
                    result.SectorId = otherExpenseDTO.SectorId;
                    result.DeletedBy = otherExpenseDTO.DeletedBy;
                    result.DeletedOn = otherExpenseDTO.DeletedOn;

                    this.UnitOfWork.Get<OtherExpense>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return otherExpenseDTO.OtherExpenseId;
            }
            return otherExpenseId;
        }

        public void MarkAsDeleted(long otherExpenseId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               dbContext.Mark_OtherExpense_AsDeleted(otherExpenseId, userId);
            }

        }
    }
}
