using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
    public interface IMachineRepairDataService
    {
        IEnumerable<MachineRepair> GetAllMachineRepairs();
        MachineRepair GetMachineRepair(long machineRepairId);
        long SaveMachineRepair(MachineRepairDTO machineRepair, string userId);
        void MarkAsDeleted(long machineRepairId, string userId);
        IEnumerable<MachineRepair> GetAllMachineRepairsForAParticularBranch(long branchId);
        IEnumerable<MachineRepair> GetAllMachineRepairsForAParticularBatch(long batchId);
    }
}
