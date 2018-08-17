using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface IBatchDataService
    {
        IEnumerable<Batch> GetAllBatches();
        Batch GetBatch(long batchId);
        long SaveBatch(BatchDTO batch, string userId);
        void MarkAsDeleted(long batchId, string userId);
        IEnumerable<Batch> GetAllBatchesForAParticularBranch(long branchId);
       // void SaveBatchGradeSize(BatchGradeSizeDTO batchGradeSizeDTO);
       // void PurgeBatchGradeSize(long batchId);
        void SaveBatchSupply(BatchSupplyDTO batchSupplyDTO);
        void PurgeBatchSupply(long batchId,long supplyId);
    }
}
