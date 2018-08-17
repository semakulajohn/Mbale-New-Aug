using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IReportService
 {
     #region transactions
     IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate);

        IEnumerable<Transaction> GenerateTransactionCurrentMonthReport();

        IEnumerable<Transaction> GenerateTransactionTodaysReport();

        IEnumerable<Transaction> GenerateTransactionCurrentWeekReport();
     #endregion

     #region supplies
         IEnumerable<Supply> GenerateSupplyCurrentMonthReport();
       
         IEnumerable<Supply> GenerateSupplyCurrentWeekReport();
       
         IEnumerable<Supply> GenerateSupplyTodaysReport();

         IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate);
       
     #endregion
 }
}
