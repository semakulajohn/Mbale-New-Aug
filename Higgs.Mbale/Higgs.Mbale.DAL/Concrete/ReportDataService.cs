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
  public  class ReportDataService : DataServiceBase, IReportDataService
    {
          ILog logger = log4net.LogManager.GetLogger(typeof(ReportDataService));

       public ReportDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
       #region Transactions
       public IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate)
       {
           return this.UnitOfWork.Get<Transaction>().AsQueryable()
               .Where(m => m.Deleted ==false &&(m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<Transaction> GenerateTransactionCurrentMonthReport()
       {
           return this.UnitOfWork.Get<Transaction>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Transaction> GenerateTransactionTodaysReport()
       {
           return this.UnitOfWork.Get<Transaction>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Transaction> GenerateTransactionCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Transaction>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }
       #endregion

       #region Supplies
       public IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate)
       {
           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<Supply> GenerateSupplyCurrentMonthReport()
       {
           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Supply> GenerateSupplyTodaysReport()
       {
           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Supply> GenerateSupplyCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }
       #endregion 
    }
}
