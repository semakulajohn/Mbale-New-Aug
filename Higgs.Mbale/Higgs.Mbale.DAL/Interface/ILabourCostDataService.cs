using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface ILabourCostDataService
    {
        IEnumerable<LabourCost> GetAllLabourCosts();
        LabourCost GetLabourCost(long labourCostId);
        long SaveLabourCost(LabourCostDTO labourCost, string userId);
        void MarkAsDeleted(long labourCostId, string userId);
        IEnumerable<LabourCost> GetAllLabourCostsForAParticularBatch(long batchId);
    }
}
