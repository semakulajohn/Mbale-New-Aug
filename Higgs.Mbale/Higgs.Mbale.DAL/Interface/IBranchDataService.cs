using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface IBranchDataService
    {
        IEnumerable<Branch> GetAllBranches();
        Branch GetBranch(long branchId);
        long SaveBranch(BranchDTO branch, string userId);
        void MarkAsDeleted(long branchId, string userId);
    }
}
