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
 public   class FactoryExpenseDataService : DataServiceBase,IFactoryExpenseDataService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(FactoryExpenseDataService));

       public FactoryExpenseDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<FactoryExpense> GetAllFactoryExpenses()
        {
            return this.UnitOfWork.Get<FactoryExpense>().AsQueryable().Where(e => e.Deleted == false); 
        }


        public IEnumerable<FactoryExpense> GetAllFactoryExpensesForAParticularBatch(long batchId)
        {
            return this.UnitOfWork.Get<FactoryExpense>().AsQueryable().Where(e => e.Deleted == false && e.BatchId == batchId);
        }

        public FactoryExpense GetFactoryExpense(long factoryExpenseId)
        {
            return this.UnitOfWork.Get<FactoryExpense>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.FactoryExpenseId == factoryExpenseId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new FactoryExpense or updates an already existing FactoryExpense.
        /// </summary>
        /// <param name="FactoryExpense">FactoryExpense to be saved or updated.</param>
        /// <param name="FactoryExpenseId">FactoryExpenseId of the FactoryExpense creating or updating</param>
        /// <returns>FactoryExpenseId</returns>
        public long SaveFactoryExpense(FactoryExpenseDTO factoryExpenseDTO, string userId)
        {
            long factoryExpenseId = 0;
            
            if (factoryExpenseDTO.FactoryExpenseId == 0)
            {
           
                var factoryExpense = new FactoryExpense()
                {
                     Amount= factoryExpenseDTO.Amount,
                    Description = factoryExpenseDTO.Description,
                    BranchId = factoryExpenseDTO.BranchId,
                    SectorId = factoryExpenseDTO.SectorId,
                    BatchId = factoryExpenseDTO.BatchId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
                     

                };

                this.UnitOfWork.Get<FactoryExpense>().AddNew(factoryExpense);
                this.UnitOfWork.SaveChanges();
                factoryExpenseId = factoryExpense.FactoryExpenseId;
                return factoryExpenseId;
            }

            else
            {
                var result = this.UnitOfWork.Get<FactoryExpense>().AsQueryable()
                    .FirstOrDefault(e => e.FactoryExpenseId == factoryExpenseDTO.FactoryExpenseId);
                if (result != null)
                {
                    result.Amount = factoryExpenseDTO.Amount;
                    result.UpdatedBy = userId;
                    result.Description = factoryExpenseDTO.Description;
                    result.BatchId = factoryExpenseDTO.BatchId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = factoryExpenseDTO.Deleted;
                    result.BranchId = factoryExpenseDTO.BranchId;
                    result.SectorId = factoryExpenseDTO.SectorId;
                    result.DeletedBy = factoryExpenseDTO.DeletedBy;
                    result.DeletedOn = factoryExpenseDTO.DeletedOn;

                    this.UnitOfWork.Get<FactoryExpense>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return factoryExpenseDTO.FactoryExpenseId;
            }
            return factoryExpenseId;
        }

        public void MarkAsDeleted(long factoryExpenseId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               dbContext.Mark_FactoryExpense_AsDeleted(factoryExpenseId, userId);
            }

        }
    }
}
