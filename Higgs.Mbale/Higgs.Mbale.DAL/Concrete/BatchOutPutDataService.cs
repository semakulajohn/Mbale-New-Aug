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
using EntityFramework.Extensions;

namespace Higgs.Mbale.DAL.Concrete
{
 public   class BatchOutPutDataService : DataServiceBase,IBatchOutPutDataService
    {
     
         ILog logger = log4net.LogManager.GetLogger(typeof(BatchOutPutDataService));

       public BatchOutPutDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<BatchOutPut> GetAllBatchOutPuts()
        {
            return this.UnitOfWork.Get<BatchOutPut>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public IEnumerable<BatchOutPut> GetAllBatchOutPutsForAParticularBatch(long batchId)
        {
            return this.UnitOfWork.Get<BatchOutPut>().AsQueryable().Where(e => e.Deleted == false && e.BatchId == batchId);
        }


        public BatchOutPut GetBatchOutPut(long batchOutPutId)
        {
            return this.UnitOfWork.Get<BatchOutPut>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.BatchOutPutId == batchOutPutId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new batchoutput or updates an already existing BatchOutPut.
        /// </summary>
        /// <param name="Batch">BatchOutPut to be saved or updated.</param>
        /// <param name="BatchId">BatchOutPutId of the BatchOutPut creating or updating</param>
        /// <returns>BatchOutPutId</returns>
        public long SaveBatchOutPut(BatchOutPutDTO batchOutPutDTO, string userId)
        {
            long batchOutPutId = 0;
            
            if (batchOutPutDTO.BatchOutPutId == 0)
            {
           
                var batchOutPut = new BatchOutPut()
                {
                    Loss = batchOutPutDTO.Loss,
                    FlourOutPut = batchOutPutDTO.FlourOutPut,
                    BrandOutPut = batchOutPutDTO.BrandOutPut,
                    BatchId = batchOutPutDTO.BatchId,
                    LossPercentage = batchOutPutDTO.LossPercentage,
                    BrandPercentage = batchOutPutDTO.BrandPercentage,
                    FlourPercentage = batchOutPutDTO.FlourPercentage,
               BranchId = batchOutPutDTO.BranchId,
               SectorId = batchOutPutDTO.SectorId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
                    
 

                };

                this.UnitOfWork.Get<BatchOutPut>().AddNew(batchOutPut);
                this.UnitOfWork.SaveChanges();
                batchOutPutId = batchOutPut.BatchOutPutId;
                            
                
                return batchOutPutId;
            }

            else
            {
                var result = this.UnitOfWork.Get<BatchOutPut>().AsQueryable()
                    .FirstOrDefault(e => e.BatchOutPutId == batchOutPutDTO.BatchOutPutId);
                if (result != null)
                {
                    result.Loss = batchOutPutDTO.Loss;
                    result.FlourOutPut = batchOutPutDTO.FlourOutPut;
                    result.BrandOutPut = batchOutPutDTO.BrandOutPut;
                    result.BatchId = batchOutPutDTO.BatchId;       
                    result.LossPercentage = batchOutPutDTO.LossPercentage;
                    result.BrandPercentage = batchOutPutDTO.BrandPercentage;
                    result.FlourPercentage = batchOutPutDTO.FlourPercentage;
                    result.SectorId = batchOutPutDTO.SectorId;
                    result.BranchId = batchOutPutDTO.BranchId;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = batchOutPutDTO.Deleted;
                    result.DeletedBy = batchOutPutDTO.DeletedBy;
                    result.DeletedOn = batchOutPutDTO.DeletedOn;

                    this.UnitOfWork.Get<BatchOutPut>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return batchOutPutDTO.BatchOutPutId;
            }           
        }

        public void MarkAsDeleted(long batchOutPutId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_Estate_And_RelatedData_AsDeleted(SectorId, userId);
            }

        }

        public void SaveBatchGradeSize(BatchGradeSizeDTO batchGradeSizeDTO)
        {
            var batchGradeSize = new BatchGradeSize()
            {
                BatchOutPutId = batchGradeSizeDTO.BatchOutPutId,
                GradeId = batchGradeSizeDTO.GradeId,
                SizeId =  batchGradeSizeDTO.SizeId,
                Quantity = batchGradeSizeDTO.Quantity,
                TimeStamp = DateTime.Now
            };
            this.UnitOfWork.Get<BatchGradeSize>().AddNew(batchGradeSize);
            this.UnitOfWork.SaveChanges();
        }

        public void PurgeBatchGradeSize(long batchOutPutId)
        {
            this.UnitOfWork.Get<BatchGradeSize>().AsQueryable()
                .Where(m => m.BatchOutPutId == batchOutPutId)
                .Delete();
        }

        public IEnumerable<BatchSupply> GetBatchSupplies(long batchId)
        {
          return  this.UnitOfWork.Get<BatchSupply>().AsQueryable()
                .Where(m => m.BatchId == batchId);
        }
       
    }
}
