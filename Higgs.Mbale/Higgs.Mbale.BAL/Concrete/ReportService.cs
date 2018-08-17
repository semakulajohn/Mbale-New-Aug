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
    public class ReportService : IReportService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(ReportService));
        private IReportDataService _dataService;
        private IUserService _userService;
        private ITransactionService _transactionService;
        private ISupplyService _supplyService;
       


        public ReportService(IReportDataService dataService, IUserService userService, ITransactionService transactionService,
            ISupplyService supplyService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionService = transactionService;
            this._supplyService = supplyService;
            
        }

        #region transactions
        public IEnumerable<Transaction> GenerateTransactionCurrentMonthReport()
        {
            var results = this._dataService.GenerateTransactionCurrentMonthReport();
            var transactionList = _transactionService.MapEFToModel(results.ToList());
            return transactionList;
        }

        public IEnumerable<Transaction> GenerateTransactionCurrentWeekReport()
        {
            var results = this._dataService.GenerateTransactionCurrentWeekReport();
            var transactionList = _transactionService.MapEFToModel(results.ToList());
            return transactionList;
        }


        public IEnumerable<Transaction> GenerateTransactionTodaysReport()
        {
            var results = this._dataService.GenerateTransactionTodaysReport();
            var transactionList = _transactionService.MapEFToModel(results.ToList());
            return transactionList;
        }

        public IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate){
            var results = this._dataService.GetAllTransactionsBetweenTheSpecifiedDates(lowerSpecifiedDate,upperSpecifiedDate);
            var transactionList = _transactionService.MapEFToModel(results.ToList());
            return transactionList;
        }

        #endregion 

        #region supplies
        public IEnumerable<Supply> GenerateSupplyCurrentMonthReport()
        {
            var results = this._dataService.GenerateSupplyCurrentMonthReport();
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }

        public IEnumerable<Supply> GenerateSupplyCurrentWeekReport()
        {
            var results = this._dataService.GenerateSupplyCurrentWeekReport();
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }


        public IEnumerable<Supply> GenerateSupplyTodaysReport()
        {
            var results = this._dataService.GenerateSupplyTodaysReport();
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }

        public IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate)
        {
            var results = this._dataService.GetAllSuppliesBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate);
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }
        #endregion



    }
}
