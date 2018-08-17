using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.DAL.Concrete;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.EF.UnitOfWork;
using Higgs.Mbale.DTO;
using log4net;

namespace Higgs.Mbale.DAL.Concrete
{
 public   class BranchDataService : DataServiceBase,IBranchDataService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(BranchDataService));

       public BranchDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Branch> GetAllBranches()
        {
            return this.UnitOfWork.Get<Branch>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public Branch GetBranch(long branchId)
        {
            return this.UnitOfWork.Get<Branch>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.BranchId == branchId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new branch or updates an already existing branch.
        /// </summary>
        /// <param name="branch">Branch to be saved or updated.</param>
        /// <param name="branchId">BranchId of the branch creating or updating</param>
        /// <returns>branchId</returns>
        public long SaveBranch(BranchDTO branchDTO, string userId)
        {
            long branchId = 0;
            
            if (branchDTO.BranchId == 0)
            {
           
                var branch = new Branch()
                {
                    Name = branchDTO.Name,
                    PhoneNumber = branchDTO.PhoneNumber,
                    Location = branchDTO.Location,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
                    MillingChargeRate = branchDTO.MillingChargeRate,
 

                };

                this.UnitOfWork.Get<Branch>().AddNew(branch);
                this.UnitOfWork.SaveChanges();
                branchId = branch.BranchId;
                return branchId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Branch>().AsQueryable()
                    .FirstOrDefault(e => e.BranchId == branchDTO.BranchId);
                if (result != null)
                {
                    result.Name = branchDTO.Name;
                    result.UpdatedBy = userId;
                    result.PhoneNumber = branchDTO.PhoneNumber;
                    result.Location = branchDTO.Location;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = branchDTO.Deleted;
                    result.MillingChargeRate = branchDTO.MillingChargeRate;
                    result.DeletedBy = branchDTO.DeletedBy;
                    result.DeletedOn = branchDTO.DeletedOn;

                    this.UnitOfWork.Get<Branch>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return branchDTO.BranchId;
            }
            return branchId;
        }

        public void MarkAsDeleted(long branchId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_Estate_And_RelatedData_AsDeleted(branchId, userId);
            }

        }
    }
}
