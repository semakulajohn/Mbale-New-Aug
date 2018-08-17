using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface IBatchOutPutDataService
    {

        IEnumerable<BatchOutPut> GetAllBatchOutPuts();
         IEnumerable<BatchOutPut> GetAllBatchOutPutsForAParticularBatch(long batchId);
          BatchOutPut GetBatchOutPut(long batchOutPutId);
          long SaveBatchOutPut(BatchOutPutDTO batchOutPutDTO, string userId);
          void MarkAsDeleted(long batchOutPutId, string userId);
          void SaveBatchGradeSize(BatchGradeSizeDTO batchGradeSizeDTO);
           void PurgeBatchGradeSize(long batchOutPutId);
           IEnumerable<BatchSupply> GetBatchSupplies(long batchId);
       
    }
}
