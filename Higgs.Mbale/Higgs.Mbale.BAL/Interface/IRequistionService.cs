using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IRequistionService
    {
        IEnumerable<Requistion> GetAllRequistions();
        Requistion GetRequistion(long requistionId);
        long SaveRequistion(Requistion requistion, string userId);
        void MarkAsDeleted(long requistionId, string userId);
        IEnumerable<Requistion> GetAllRequistionsForAParticularStatus(long statusId);
        IEnumerable<Requistion> GetAllRequistionsForAParticularBranch(long branchId);
    }
}
