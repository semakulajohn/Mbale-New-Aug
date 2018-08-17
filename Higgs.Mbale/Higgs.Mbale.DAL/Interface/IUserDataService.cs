using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.DTO;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface IUserDataService
    {

        AspNetUser GetLoggedInUser(string userId);
        bool UserExists(string finder);
        AspNetUser SaveUser(AspNetUserDTO user, string userId);
        bool MarkAsDeleted(string Id);
        AspNetUser GetAspNetUser(string Id);
        IEnumerable<AspNetRole> GetAllRoles();
        AspNetRole GetAspNetRole(string roleId);
        IEnumerable<AspNetUser> GetAspNetUsers();
        void CreateAspNetUserRolesRecord(string userId, string roleId);
        void SaveUserBranch(UserBranchDTO userBranchDTO);
       UserBranch GetUserBranch(string branchManagerId);
    }
}
