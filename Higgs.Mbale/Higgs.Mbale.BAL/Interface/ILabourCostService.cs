using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface ILabourCostService
    {
        IEnumerable<LabourCost> GetAllLabourCosts();
        LabourCost GetLabourCost(long labourCostId);
        long SaveLabourCost(LabourCost labourCost, string userId);
        void MarkAsDeleted(long labourCostId, string userId);
        IEnumerable<LabourCost> GetAllLabourCostsForAParticularBatch(long batchId);
    }
}
