using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface IReportDataService
  {
      #region transactions
      IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate);

         IEnumerable<Transaction> GenerateTransactionCurrentMonthReport();

         IEnumerable<Transaction> GenerateTransactionTodaysReport();

         IEnumerable<Transaction> GenerateTransactionCurrentWeekReport();
      #endregion

         #region supplies
         IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate);
        
         IEnumerable<Supply> GenerateSupplyCurrentMonthReport();
        
          IEnumerable<Supply> GenerateSupplyTodaysReport();

          IEnumerable<Supply> GenerateSupplyCurrentWeekReport();
         
#endregion
  }
}
