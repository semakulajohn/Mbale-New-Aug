using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;
using log4net;

namespace Higgs.Mbale.BAL.Concrete
{
   public class UserService : IUserService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(UserService));
        private IUserDataService _dataService;

        public UserService(IUserDataService dataService)
        {
            this._dataService = dataService;
        }


        public AspNetUser GetLoggedInUser(string userId)
        {
            var result = this._dataService.GetLoggedInUser(userId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// Converts an AspNetUser Models object to an AspNetUser DTO Object and passes the AspNetUser DTO
        /// along with the userId to the SaveUser Method in DAL for saving.
        /// </summary>
        /// <param name="user">AspNetUser Models object.</param>
        /// <param name="userId">UserId of the user saving the AspNetUser.</param>
        /// <returns>AspNetUser Models object.</returns>
        public AspNetUser SaveUser(AspNetUser user, string userId)
        {
            var aspNetUserDTO = new DTO.AspNetUserDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Mobile = user.Mobile,
                GenderId = user.GenderId,
                DateOfBirth = user.DateOfBirth,
                PasswordHash = user.PasswordHash,
                UserName = user.UserName,

            };
            var result = this._dataService.SaveUser(aspNetUserDTO, userId);

            return MapEFToModel(result);
        }


        /// <summary>
        /// checks whether the user with the specified email or userId exists,
        /// returns true if user exists or false.
        /// </summary>
        /// <param name="finder">EmailAddress or UserId of the user to check.</param>
        /// <returns>True or False</returns>
        public bool UserExists(string finder)
        {
            return this._dataService.UserExists(finder);
        }


        /// <summary>
        /// Gets the full name of AspNetUser by concatenating its FirstName and lastName
        /// and returns the full name.
        /// </summary>
        /// <param name="aspNetUser">AspNetUser EF Models passed to obtain full Name from. </param>
        /// <returns>AspNetUser fullname which is a string resulting from the concatenation of first name and last name of the aspnet user.</returns>
        public string GetUserFullName(EF.Models.AspNetUser aspNetUser)
        {
            string fullName = string.Empty;
            if (aspNetUser != null)
            {
                fullName = string.Concat(aspNetUser.FirstName, " ", aspNetUser.LastName);
            }
            return fullName;
        }

        public AspNetUser GetAspNetUser(string Id)
        {
            var result = _dataService.GetAspNetUser(Id);
            if (result == null)
            {
                AspNetUser user = null;
                return user;
            }
            else
            {
                return MapEFToModel(result);
            }
        }

        public IEnumerable<AspNetUser> GetAllAspNetUsers()
        {
            var results = this._dataService.GetAspNetUsers();
            return MapEFToModel(results);
        }

        public IEnumerable<AspNetUser> GetAllAdmins()
        {
            var results = this._dataService.GetAspNetUsers();
            List<EF.Models.AspNetUser> admins = new List<EF.Models.AspNetUser>();
            if (results.Any())
            {
                foreach (var result in results)
                {
                    var roleName = string.Empty;
                    var roles = result.AspNetRoles.ToList();
                    foreach (var role in roles)
                    {
                        roleName = role.Name;
                    }
                    if (roleName == "admin")
                    {
                        admins.Add(result);
                    }

                }
                return MapEFToModel(admins);
            }
            return null;

        }
        public IEnumerable<AspNetUser> GetAllCustomers()
        {
            var results = this._dataService.GetAspNetUsers();
            List<EF.Models.AspNetUser> customers = new List<EF.Models.AspNetUser>();
            if (results.Any())
            {
                foreach (var result in results)
                {
                    var roleName = string.Empty;
                    var roles = result.AspNetRoles.ToList();
                    foreach (var role in roles)
                    {
                        roleName = role.Name;
                    }
                    if (roleName == "customer")
                    {
                        customers.Add(result);
                    }

                }
                return MapEFToModel(customers);
            }
            return null;

        }

        public IEnumerable<AspNetUser> GetAllSuppliers()
        {
            var results = this._dataService.GetAspNetUsers();
            List<EF.Models.AspNetUser> suppliers = new List<EF.Models.AspNetUser>();
            if (results.Any())
            {
                foreach (var result in results)
                {
                    var roleName = string.Empty;
                    var roles = result.AspNetRoles.ToList();
                    foreach (var role in roles)
                    {
                        roleName = role.Name;
                    }
                    if (roleName == "supplier")
                    {
                        suppliers.Add(result);
                    }

                }
                return MapEFToModel(suppliers);
            }
            return null;

        }
        public IEnumerable<AspNetUser> GetAllBranchManagers()
        {
            var results = this._dataService.GetAspNetUsers();
            List<EF.Models.AspNetUser> managers = new List<EF.Models.AspNetUser>();
            if (results.Any())
            {
                foreach (var result in results)
                {
                    var roleName = string.Empty;
                    var roles = result.AspNetRoles.ToList();
                    foreach (var role in roles)
                    {
                        roleName = role.Name;
                    }
                    if (roleName == "branchManager")
                    {
                        managers.Add(result);
                    }

                }
                return MapEFToModel(managers);
            }
            return null;

        }

        public IEnumerable<AspNetRole> GetAllRoles()
        {
            var results = _dataService.GetAllRoles();
            return MapEFRoleToModelRole(results);
        }

        public AspNetRole GetAspNetRole(string roleId)
        {
            var result = _dataService.GetAspNetRole(roleId);
            return MapEFRoleToModelRole(result);
        }

        #region Mapping Methods
        /// <summary>
        /// Maps An IEnumerable collection of EF AspNetUser objects to An IEnumerable collection of AspNetUser Models Objects.
        /// </summary>
        /// <param name="data">An IEnumerable collection of AspNetUser EF objects.</param>
        /// <returns>An IEnumerable collection of AspNetUser Models objects.</returns>
        private IEnumerable<AspNetUser> MapEFToModel(IEnumerable<EF.Models.AspNetUser> data)
        {
            var list = new List<AspNetUser>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps AspNetUser EF Object to AspNetUser Models Object
        /// and returns AspNetUser Model object.
        /// </summary>
        /// <param name="data">AspNet EF Object.</param>
        /// <returns>AspNet Models Object</returns>
        private AspNetUser MapEFToModel(EF.Models.AspNetUser data)
        {
            var user = new AspNetUser()
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                UserName = data.UserName,
                MiddleName = data.MiddleName,

                PhoneNumber = data.PhoneNumber,
                Mobile = data.Mobile,
                PasswordHash = data.PasswordHash,
                GenderId = data.GenderId,
                CreatedBy = data.CreatedBy,
                UpdatedBy = data.UpdatedBy,
                TimeStamp = data.TimeStamp,
                DateOfBirth = data.DateOfBirth,
                CreatedOn = data.CreatedOn,
                RoleName = data.AspNetRoles.FirstOrDefault().Name
            };


            List<string> userroles = new List<string>();

            var dbUserRoles = data.AspNetRoles;
            var numberOfRoles = dbUserRoles.Count;
            EF.Models.AspNetRole[] roles = new EF.Models.AspNetRole[numberOfRoles];
            if (dbUserRoles != null)
            {
                dbUserRoles.CopyTo(roles, 0);
                foreach (var role in roles)
                {
                    userroles.Add(role.Name);
                }
            }
            user.UserRoles = userroles;
            return user;
        }




        private IEnumerable<AspNetRole> MapEFRoleToModelRole(IEnumerable<EF.Models.AspNetRole> data)
        {
            var list = new List<AspNetRole>();
            foreach (var result in data)
            {
                list.Add(MapEFRoleToModelRole(result));
            }
            return list;
        }


        private AspNetRole MapEFRoleToModelRole(EF.Models.AspNetRole data)
        {
            var role = new AspNetRole()
            {
                Id = data.Id,
                Name = data.Name,
            };
            return role;
        }

        private IEnumerable<EF.Models.AspNetRole> MapModelRoleToEFRole(IEnumerable<AspNetRole> data)
        {
            var list = new List<EF.Models.AspNetRole>();
            foreach (var result in data)
            {
                list.Add(MapModelRoleToEFRole(result));
            }
            return list;
        }


        private EF.Models.AspNetRole MapModelRoleToEFRole(AspNetRole data)
        {
            var role = new EF.Models.AspNetRole()
            {
                Id = data.Id,
                Name = data.Name,
            };
            return role;
        }

        public UserBranch MapEFToModel(EF.Models.UserBranch data)
        {

            var userBranch = new UserBranch()
            {
                BranchId = data.BranchId,
                UserId = data.UserId,
                BranchName = data.Branch != null ? data.Branch.Name : "",
               };
            return userBranch;
        }

        #endregion

         public void SaveUserBranch(string userId,long branchId){
       var userBranchDTO = new UserBranchDTO()
       {
           UserId = userId,
           BranchId = branchId,
       };
       
       this._dataService.SaveUserBranch(userBranchDTO);

   }
         public UserBranch GetBranchManager(string branchManagerId)
         {
            
             var result = _dataService.GetUserBranch(branchManagerId);
             return MapEFToModel(result);

         }

        public bool MarkAsDeleted(string Id)
        {
            return _dataService.MarkAsDeleted(Id);
        }
    }
}
