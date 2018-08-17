using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;
using Higgs.Mbale.EF;

namespace Higgs.Mbale.BAL.Interface
{
  public  interface IUserService
    {
        AspNetUser GetLoggedInUser(string userId);
        bool UserExists(string finder);
        AspNetUser SaveUser(AspNetUser user, string userId);
        string GetUserFullName(EF.Models.AspNetUser aspNetUser);
        
        bool MarkAsDeleted(string Id);
        AspNetUser GetAspNetUser(string Id);
        IEnumerable<AspNetRole> GetAllRoles();
        IEnumerable<AspNetUser> GetAllAspNetUsers();
        AspNetRole GetAspNetRole(string roleId);
        IEnumerable<AspNetUser> GetAllBranchManagers();
        IEnumerable<AspNetUser> GetAllAdmins();
        IEnumerable<AspNetUser> GetAllSuppliers();
        IEnumerable<AspNetUser> GetAllCustomers();

        void SaveUserBranch(string userId, long branchId);
      UserBranch GetBranchManager(string branchManagerId);
    }
}
