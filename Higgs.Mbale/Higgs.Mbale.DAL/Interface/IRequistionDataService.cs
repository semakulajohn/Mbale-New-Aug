using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface IRequistionDataService
    {
        IEnumerable<Requistion> GetAllRequistions();
        Requistion GetRequistion(long requistionId);
        long SaveRequistion(RequistionDTO requistion, string userId);
        void MarkAsDeleted(long requistionId, string userId);
        IEnumerable<Requistion> GetAllRequistionsForAParticularStatus(long statusId);
        IEnumerable<Requistion> GetAllRequistionsForAParticularBranch(long branchId);
    }
}
