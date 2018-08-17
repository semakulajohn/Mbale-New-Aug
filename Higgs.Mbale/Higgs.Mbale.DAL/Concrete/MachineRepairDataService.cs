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
    public class MachineRepairDataService: DataServiceBase,IMachineRepairDataService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(MachineRepairDataService));

       public MachineRepairDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

       public IEnumerable<MachineRepair> GetAllMachineRepairs()
        {
            return this.UnitOfWork.Get<MachineRepair>()
                .AsQueryable().Where(e => e.Deleted == false); 
        }

        public MachineRepair GetMachineRepair(long machineRepairId)
        {
            return this.UnitOfWork.Get<MachineRepair>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.MachineRepairId == machineRepairId &&
                    c.Deleted == false
                );
        }
        public IEnumerable<MachineRepair> GetAllMachineRepairsForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<MachineRepair>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }

        public IEnumerable<MachineRepair> GetAllMachineRepairsForAParticularBatch(long batchId)
        {
            return this.UnitOfWork.Get<MachineRepair>().AsQueryable().Where(e => e.Deleted == false && e.BatchId == batchId);
        }
        /// <summary>
        /// Saves a new MachineRepair or updates an already existing MachineRepair.
        /// </summary>
        /// <param name="MachineRepair">MachineRepair to be saved or updated.</param>
        /// <param name="MachineRepairId">MachineRepairId of the MachineRepair creating or updating</param>
        /// <returns>MachineRepairId</returns>
        public long SaveMachineRepair(MachineRepairDTO machineRepairDTO, string userId)
        {
            long machineRepairId = 0;
            
            if (machineRepairDTO.MachineRepairId == 0)
            {
                var machineRepair = new MachineRepair()
                {
                   
                    Amount = machineRepairDTO.Amount,
                    NameOfRepair = machineRepairDTO.NameOfRepair,
                    BranchId = machineRepairDTO.BranchId, 
                    SectorId = machineRepairDTO.SectorId,
                    BatchId = machineRepairDTO.BatchId,
                    TransactionSubTypeId = machineRepairDTO.TransactionSubTypeId,
                    Description = machineRepairDTO.Description,
                    DateRepaired = machineRepairDTO.DateRepaired,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false, 
                };

                this.UnitOfWork.Get<MachineRepair>().AddNew(machineRepair);
                this.UnitOfWork.SaveChanges();
                machineRepairId = machineRepair.MachineRepairId;
                return machineRepairId;
            }

            else
            {
                var result = this.UnitOfWork.Get<MachineRepair>().AsQueryable()
                    .FirstOrDefault(e => e.MachineRepairId == machineRepairDTO.MachineRepairId);
                if (result != null)
                {
                   
                    result.Amount = machineRepairDTO.Amount;
                    result.DateRepaired =  machineRepairDTO.DateRepaired;
                    result.NameOfRepair = machineRepairDTO.NameOfRepair;
                    result.SectorId = machineRepairDTO.SectorId;
                    result.BranchId = machineRepairDTO.BranchId;
                    result.BatchId = machineRepairDTO.BatchId;
                    result.TransactionSubTypeId = machineRepairDTO.TransactionSubTypeId;
                    result.Description = machineRepairDTO.Description;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    this.UnitOfWork.Get<MachineRepair>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return machineRepairDTO.MachineRepairId;
            }            
        }

        public void MarkAsDeleted(long machineRepairId, string userId)
        {
            using (var dbContext = new MbaleEntities())
            {
                dbContext.Mark_MachineRepair_AsDeleted(machineRepairId, userId);
            }

        }
    }
}
