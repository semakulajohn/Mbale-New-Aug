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
public    class LabourCostDataService : DataServiceBase,ILabourCostDataService
    {
     ILog logger = log4net.LogManager.GetLogger(typeof(LabourCostDataService));

       public LabourCostDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<LabourCost> GetAllLabourCosts()
        {
            return this.UnitOfWork.Get<LabourCost>().AsQueryable().Where(e => e.Deleted == false); 
        }


        public IEnumerable<LabourCost> GetAllLabourCostsForAParticularBatch(long batchId)
        {
            return this.UnitOfWork.Get<LabourCost>().AsQueryable().Where(e => e.Deleted == false && e.BatchId == batchId);
        }

        public LabourCost GetLabourCost(long labourCostId)
        {
            return this.UnitOfWork.Get<LabourCost>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.LabourCostId == labourCostId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new LabourCost or updates an already existing LabourCost.
        /// </summary>
        /// <param name="LabourCost">LabourCost to be saved or updated.</param>
        /// <param name="LabourCostId">LabourCostId of the LabourCost creating or updating</param>
        /// <returns>LabourCostId</returns>
        public long SaveLabourCost(LabourCostDTO labourCostDTO, string userId)
        {
            long labourCostId = 0;
            
            if (labourCostDTO.LabourCostId == 0)
            {
           
                var labourCost = new LabourCost()
                {
                    Rate = labourCostDTO.Rate,
                    Quantity = labourCostDTO.Quantity,
                     Amount= labourCostDTO.Amount,
                    ActivityId = labourCostDTO.ActivityId,
                    BranchId = labourCostDTO.BranchId,
                    SectorId = labourCostDTO.SectorId,
                    BatchId = labourCostDTO.BatchId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
                     

                };

                this.UnitOfWork.Get<LabourCost>().AddNew(labourCost);
                this.UnitOfWork.SaveChanges();
                labourCostId = labourCost.LabourCostId;
                return labourCostId;
            }

            else
            {
                var result = this.UnitOfWork.Get<LabourCost>().AsQueryable()
                    .FirstOrDefault(e => e.LabourCostId == labourCostDTO.LabourCostId);
                if (result != null)
                {
                    result.Amount = labourCostDTO.Amount;
                    result.UpdatedBy = userId;
                    result.Rate = labourCostDTO.Rate;
                    result.BatchId = labourCostDTO.BatchId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = labourCostDTO.Deleted;
                    result.BranchId = labourCostDTO.BranchId;
                    result.SectorId = labourCostDTO.SectorId;
                    result.DeletedBy = labourCostDTO.DeletedBy;
                    result.DeletedOn = labourCostDTO.DeletedOn;
                    result.Quantity = labourCostDTO.Quantity;

                    this.UnitOfWork.Get<LabourCost>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return labourCostDTO.LabourCostId;
            }
            return labourCostId;
        }

        public void MarkAsDeleted(long labourCostId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
                dbContext.Mark_LabourCost_AsDeleted(labourCostId, userId);
               
            }

        }
    }
}
