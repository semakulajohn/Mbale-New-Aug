using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
    public interface IMachineRepairService
    {
        IEnumerable<MachineRepair> GetAllMachineRepairs();
        MachineRepair GetMachineRepair(long machineRepairId);
        long SaveMachineRepair(MachineRepair machineRepair, string userId);
        void MarkAsDeleted(long machineRepairId, string userId);
        IEnumerable<MachineRepair> GetAllMachineRepairsForAParticularBranch(long branchId);
        IEnumerable<MachineRepair> GetAllMachineRepairsForAParticularBatch(long batchId);
    }
}
