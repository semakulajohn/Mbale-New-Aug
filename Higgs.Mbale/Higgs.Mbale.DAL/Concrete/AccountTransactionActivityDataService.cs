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
 public   class AccountTransactionActivityDataService : DataServiceBase,IAccountTransactionActivityDataService
    {
        
       ILog logger = log4net.LogManager.GetLogger(typeof(AccountTransactionActivityDataService));

       public AccountTransactionActivityDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

     

       public IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivities()
        {
            return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable().Where(e => e.Deleted == false); 
        }

       public AccountTransactionActivity GetAccountTransactionActivity(long accountTransactionActivityId)
        {
            return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.AccountTransactionActivityId == accountTransactionActivityId &&
                    c.Deleted == false
                );
        }
       public IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivitiesForAParticularAspNetUser(string accountId)
       {
           
            return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable().Where(e => e.Deleted == false && e.AspNetUserId == accountId   );
        }
       public IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivitiesForAParticularCasualWorker(long accountId)
       {

           return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable().Where(e => e.Deleted == false && e.CasualWorkerId == accountId);
       }

       public AccountTransactionActivity GetLatestAccountTransactionActivityForAParticularAspNetUser(string accountId)
       {
           AccountTransactionActivity accountTransactionActivity = new AccountTransactionActivity();
           var accountTransactionActivities = this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable().Where(e => e.AspNetUserId == accountId);
           if (accountTransactionActivities.Any())
           {
                accountTransactionActivity = accountTransactionActivities.AsQueryable().OrderByDescending(e => e.CreatedOn).First();
                return accountTransactionActivity;
           }
           else
           {
               return accountTransactionActivity;
           }
          
       }
       public AccountTransactionActivity GetLatestAccountTransactionActivitiesForAParticularCasualWorker(long casualWorkerId)
       {

           AccountTransactionActivity accountTransactionActivity = new AccountTransactionActivity();
           var accountTransactionActivities = this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable().Where(e => e.CasualWorkerId == casualWorkerId);
           if (accountTransactionActivities.Any())
           {
              accountTransactionActivity = accountTransactionActivities.AsQueryable().OrderByDescending(e => e.CreatedOn).First();
               return accountTransactionActivity;
           }
           else
           {
               return accountTransactionActivity;
           }
       }
                

       public long SaveAccountTransactionActivity(AccountTransactionActivityDTO accountTransactionActivityDTO, string userId)
        {
            long accountTransactionActivityId = 0;

            if (accountTransactionActivityDTO.AccountTransactionActivityId == 0)
            {

                var accountTransactionActivity = new AccountTransactionActivity()
                {
                    AspNetUserId = accountTransactionActivityDTO.AspNetUserId,
                    CasualWorkerId = accountTransactionActivityDTO.CasualWorkerId,
                    Amount = accountTransactionActivityDTO.Amount,
                    StartAmount = accountTransactionActivityDTO.StartAmount,
                    Notes  = accountTransactionActivityDTO.Notes,
                    Action = accountTransactionActivityDTO.Action,
                    Balance = accountTransactionActivityDTO.Balance,
                    TransactionSubTypeId = accountTransactionActivityDTO.TransactionSubTypeId,
                    BranchId = accountTransactionActivityDTO.BranchId,
                    SectorId = accountTransactionActivityDTO.SectorId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false, 
                };

                this.UnitOfWork.Get<AccountTransactionActivity>().AddNew(accountTransactionActivity);
                this.UnitOfWork.SaveChanges();
                accountTransactionActivityId = accountTransactionActivity.AccountTransactionActivityId;
                return accountTransactionActivityId;
            }

            else
            {
                var result = this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable()
                    .FirstOrDefault(e => e.AccountTransactionActivityId == accountTransactionActivityDTO.AccountTransactionActivityId);
                if (result != null)
                {
                    result.Action = accountTransactionActivityDTO.Action;
                    result.Amount = accountTransactionActivityDTO.Amount;
                    result.AspNetUserId = accountTransactionActivityDTO.AspNetUserId;
                    result.CasualWorkerId = accountTransactionActivityDTO.CasualWorkerId;
                    result.StartAmount = accountTransactionActivityDTO.StartAmount;
                    result.Balance = accountTransactionActivityDTO.Balance;
                    result.Notes = accountTransactionActivityDTO.Notes;
                    result.TransactionSubTypeId = accountTransactionActivityDTO.TransactionSubTypeId;
                    result.BranchId = accountTransactionActivityDTO.BranchId;
                    result.SectorId = accountTransactionActivityDTO.SectorId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = accountTransactionActivityDTO.Deleted;
                    result.DeletedBy = accountTransactionActivityDTO.DeletedBy;
                    result.DeletedOn = accountTransactionActivityDTO.DeletedOn;

                    this.UnitOfWork.Get<AccountTransactionActivity>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return accountTransactionActivityDTO.AccountTransactionActivityId;
            }            
        }

       public void MarkAsDeleted(long accountTransactionActivityId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
              //TODO: THROW NOT IMPLEMENTED EXCEPTION
            }

        }

       public IEnumerable<PaymentMode> GetAllPaymentModes()
       {
           return this.UnitOfWork.Get<PaymentMode>().AsQueryable().Where(e => e.Deleted == false);
       }
    }
}
