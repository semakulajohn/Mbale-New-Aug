using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DAL.Interface;
using log4net;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.EF.UnitOfWork;
using Higgs.Mbale.DTO;

namespace Higgs.Mbale.DAL.Concrete
{
 public   class TransactionSubTypeDataService : DataServiceBase,ITransactionSubTypeDataService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(TransactionSubTypeDataService));

       public TransactionSubTypeDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<TransactionSubType> GetAllTransactionSubTypes()
        {
            return this.UnitOfWork.Get<TransactionSubType>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public IEnumerable<TransactionType> GetAllTransactionTypes()
        {
            return this.UnitOfWork.Get<TransactionType>().AsQueryable();
        }
        public TransactionSubType GetTransactionSubType(long transactionSubTypeId)
        {
            return this.UnitOfWork.Get<TransactionSubType>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.TransactionSubTypeId == transactionSubTypeId &&
                    c.Deleted == false
                );
        }

        //public TransactionSubType GetTransactionTypeIdForParticularSubType(long transactionSubTypeId)
        //{
        //    return this.UnitOfWork.Get<TransactionSubType>().AsQueryable()
        //         .FirstOrDefault(c =>
        //            c.TransactionTypeId == transactionSubTypeId &&
        //            c.Deleted == false
        //        );
        //}

        /// <summary>
        /// Saves a new TransactionSubType or updates an already existing TransactionSubType.
        /// </summary>
        /// <param name="TransactionSubType">TransactionSubType to be saved or updated.</param>
        /// <param name="TransactionSubTypeId">TransactionSubTypeId of the TransactionSubType creating or updating</param>
        /// <returns>TransactionSubTypeId</returns>
        public long SaveTransactionSubType(TransactionSubTypeDTO transactionSubTypeDTO, string userId)
        {
            long transactionSubTypeId = 0;
            
            if (transactionSubTypeDTO.TransactionSubTypeId == 0)
            {
           
                var transactionSubType = new TransactionSubType()
                {
                    TransactionSubTypeId = transactionSubTypeDTO.TransactionSubTypeId,
                    TransactionTypeId = transactionSubTypeDTO.TransactionTypeId,
                    Name = transactionSubTypeDTO.Name,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<TransactionSubType>().AddNew(transactionSubType);
                this.UnitOfWork.SaveChanges();
                transactionSubTypeId = transactionSubType.TransactionSubTypeId;
                return transactionSubTypeId;
            }

            else
            {
                var result = this.UnitOfWork.Get<TransactionSubType>().AsQueryable()
                    .FirstOrDefault(e => e.TransactionSubTypeId == transactionSubTypeDTO.TransactionSubTypeId);
                if (result != null)
                {
                    result.TransactionSubTypeId = transactionSubTypeDTO.TransactionSubTypeId;
                    result.Name = transactionSubTypeDTO.Name;
                    result.TransactionTypeId = transactionSubTypeDTO.TransactionTypeId;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = transactionSubTypeDTO.Deleted;
                    result.DeletedBy = transactionSubTypeDTO.DeletedBy;
                    result.DeletedOn = transactionSubTypeDTO.DeletedOn;

                    this.UnitOfWork.Get<TransactionSubType>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return transactionSubTypeDTO.TransactionSubTypeId;
            }           
        }

        public void MarkAsDeleted(long transactionSubTypeId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_Estate_And_RelatedData_AsDeleted(SectorId, userId);
            }

        }
    }
}
