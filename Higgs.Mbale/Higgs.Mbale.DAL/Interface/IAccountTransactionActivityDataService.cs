using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
    public interface IAccountTransactionActivityDataService
    {
        AccountTransactionActivity GetAccountTransactionActivity(long accountTransactionActivityId);
        IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivities();

        IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivitiesForAParticularAspNetUser(string accountId);

        IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivitiesForAParticularCasualWorker(long accountId);
        
        long SaveAccountTransactionActivity(AccountTransactionActivityDTO accountTransactionActivityDTO, string userId);

        void MarkAsDeleted(long accountTransactionActivityId, string userId);
        AccountTransactionActivity GetLatestAccountTransactionActivityForAParticularAspNetUser(string accountId);
        AccountTransactionActivity GetLatestAccountTransactionActivitiesForAParticularCasualWorker(long casualWorkerId);
        IEnumerable<PaymentMode> GetAllPaymentModes();
                
        

    }
}
