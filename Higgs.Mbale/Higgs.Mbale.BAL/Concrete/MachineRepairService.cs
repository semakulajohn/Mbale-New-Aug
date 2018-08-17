using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;
using log4net;

namespace Higgs.Mbale.BAL.Concrete
{
    public class MachineRepairService: IMachineRepairService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(MachineRepairService));
        private IMachineRepairDataService _dataService;
        private IUserService _userService;
        private ITransactionDataService _transactionDataService;
        private ITransactionSubTypeService _transactionSubTypeService;



        public MachineRepairService(IMachineRepairDataService dataService, IUserService userService, ITransactionDataService transactionDataService, ITransactionSubTypeService transactionSubTypeService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionDataService = transactionDataService;
            this._transactionSubTypeService = transactionSubTypeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MachineRepairId"></param>
        /// <returns></returns>
        public MachineRepair GetMachineRepair(long machineRepairId)
        {
            var result = this._dataService.GetMachineRepair(machineRepairId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MachineRepair> GetAllMachineRepairs()
        {
            var results = this._dataService.GetAllMachineRepairs();
            return MapEFToModel(results);
        } 

       
        public long SaveMachineRepair(MachineRepair machineRepair, string userId)
        {
            var machineRepairDTO = new DTO.MachineRepairDTO()
            {
               
                Amount = machineRepair.Amount,
                NameOfRepair = machineRepair.NameOfRepair,
                DateRepaired = machineRepair.DateRepaired,
                BranchId = machineRepair.BranchId,
                BatchId = machineRepair.BatchId,
                TransactionSubTypeId = machineRepair.TransactionSubTypeId,
                Description = machineRepair.Description,
                SectorId = machineRepair.SectorId,
                MachineRepairId = machineRepair.MachineRepairId,                
                Deleted = machineRepair.Deleted,
                CreatedBy = machineRepair.CreatedBy,
                CreatedOn = machineRepair.CreatedOn
            };

           var machineRepairId = this._dataService.SaveMachineRepair(machineRepairDTO, userId);
           if (machineRepair.MachineRepairId == 0)
           {

               long transactionTypeId = 0;
               var transactionSubtype = _transactionSubTypeService.GetTransactionSubType(machineRepairDTO.TransactionSubTypeId);
               if (transactionSubtype != null)
               {
                   transactionTypeId = transactionSubtype.TransactionTypeId;
               }

               var transaction = new TransactionDTO()
               {
                   BranchId = machineRepairDTO.BranchId,
                   SectorId = machineRepairDTO.SectorId,
                   Amount = machineRepair.Amount,
                   TransactionSubTypeId = machineRepairDTO.TransactionSubTypeId,
                   TransactionTypeId = transactionTypeId,
                   CreatedOn = DateTime.Now,
                   TimeStamp = DateTime.Now,
                   CreatedBy = userId,
                   Deleted = false,

               };
               var transactionId = _transactionDataService.SaveTransaction(transaction, userId);
           }
           return machineRepairId;                      
        }

        public IEnumerable<MachineRepair> GetAllMachineRepairsForAParticularBatch(long batchId)
        {
            var results = this._dataService.GetAllMachineRepairsForAParticularBatch(batchId);
            return MapEFToModel(results);
        }

        public IEnumerable<MachineRepair> GetAllMachineRepairsForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllMachineRepairsForAParticularBranch(branchId);
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="machineRepairId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long machineRepairId, string userId)
        {
            _dataService.MarkAsDeleted(machineRepairId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<MachineRepair> MapEFToModel(IEnumerable<EF.Models.MachineRepair> data)
        {
            var list = new List<MachineRepair>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps MachineRepair EF object to MachineRepair Model Object and
        /// returns the MachineRepair model object.
        /// </summary>
        /// <param name="result">EF MachineRepair object to be mapped.</param>
        /// <returns>MachineRepair Model Object.</returns>
        public MachineRepair MapEFToModel(EF.Models.MachineRepair data)
        {
            var machineRepair = new MachineRepair()
            {
               
                Amount = data.Amount,
                NameOfRepair = data.NameOfRepair,
                BranchId = data.BranchId,
                Description = data.Description,
                SectorId = data.SectorId,
                BatchId = data.BatchId,
                DateRepaired = data.DateRepaired,
                TransactionSubTypeId = data.TransactionSubTypeId,
                TransactionSubTypeName = data.TransactionSubType !=null? data.TransactionSubType.Name:"",
                BranchName = data.Branch !=null? data.Branch.Name:"",
                SectorName = data.Sector != null ? data.Sector.Name : "",
                MachineRepairId = data.MachineRepairId,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1)              
            };
            return machineRepair;
        }

       #endregion
    }
}
