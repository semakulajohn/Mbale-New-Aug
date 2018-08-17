using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IAccountTransactionActivityService
    {
        IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivities();
        AccountTransactionActivity GetAccountTransactionActivity(long accountTransactionActivityId);
        long SaveAccountTransactionActivity(AccountTransactionActivity accountTransactionActivity, string userId);
        void MarkAsDeleted(long accountTransactionActivityId, string userId);

        IEnumerable<AccountTransactionActivity> GetAllAccountTransactionActivitiesForAParticularAccount(string accountId);
        IEnumerable<PaymentMode> GetAllPaymentModes();
       


    }
}
