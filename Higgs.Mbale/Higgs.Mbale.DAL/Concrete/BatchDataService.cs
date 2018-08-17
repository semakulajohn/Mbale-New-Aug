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
  public  class BatchDataService : DataServiceBase, IBatchDataService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(BatchDataService));

       public BatchDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Batch> GetAllBatches()
        {
            return this.UnitOfWork.Get<Batch>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public IEnumerable<Batch> GetAllBatchesForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Batch>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }


        public Batch GetBatch(long batchId)
        {
            return this.UnitOfWork.Get<Batch>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.BatchId == batchId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new batch or updates an already existing Batch.
        /// </summary>
        /// <param name="Batch">Batch to be saved or updated.</param>
        /// <param name="BatchId">BatchId of the Batch creating or updating</param>
        /// <returns>BatchId</returns>
        public long SaveBatch(BatchDTO batchDTO, string userId)
        {
            long batchId = 0;
            
            if (batchDTO.BatchId == 0)
            {
           
                var batch = new Batch()
                {
                    Name = batchDTO.Name,
                    SectorId = batchDTO.SectorId,
                    Quantity = batchDTO.Quantity,
                   
                    BranchId = batchDTO.BranchId,
                      
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };                                            

                this.UnitOfWork.Get<Batch>().AddNew(batch);
                this.UnitOfWork.SaveChanges();
                batchId = batch.BatchId;
                            
                
                return batchId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Batch>().AsQueryable()
                    .FirstOrDefault(e => e.BatchId == batchDTO.BatchId);
                if (result != null)
                {
                    result.Name = batchDTO.Name;
                    result.Quantity = batchDTO.Quantity;
                    result.BranchId = batchDTO.BranchId;
                    result.SectorId = batchDTO.SectorId;
                             
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = batchDTO.Deleted;
                    result.DeletedBy = batchDTO.DeletedBy;
                    result.DeletedOn = batchDTO.DeletedOn;

                    this.UnitOfWork.Get<Batch>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return batchDTO.BatchId;
            }           
        }

        public void MarkAsDeleted(long batchId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_Estate_And_RelatedData_AsDeleted(SectorId, userId);
            }

        }

     

        public void SaveBatchSupply(BatchSupplyDTO batchSupplyDTO)
        {
            var batchSupply = new BatchSupply()
            {
                BatchId = batchSupplyDTO.BatchId,
                SupplyId =Convert.ToInt64(batchSupplyDTO.SupplyId),
                Quantity = Convert.ToDouble(batchSupplyDTO.Quantity),
                CreatedOn = DateTime.Now
            };
            this.UnitOfWork.Get<BatchSupply>().AddNew(batchSupply);
            this.UnitOfWork.SaveChanges();
        }

        public void PurgeBatchSupply(long batchId,long supplyId)
        {
            this.UnitOfWork.Get<BatchSupply>().AsQueryable()
                .Where(m => m.BatchId == batchId && m.SupplyId == supplyId)
                .Delete();
        }
    }
}
