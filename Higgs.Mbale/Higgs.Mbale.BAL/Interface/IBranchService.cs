using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IBranchService
    {
        IEnumerable<Branch> GetAllBranches();
        Branch GetBranch(long branchId);
        long SaveBranch(Branch branch, string userId);
        void MarkAsDeleted(long branchId, string userId);
     
    }
}
