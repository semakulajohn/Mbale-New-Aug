using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Higgs.Mbale.BAL.Interface;
using log4net;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.Web.Controllers
{
    public class ReportApiController : ApiController
    {
          private IReportService _reportService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(ReportApiController));
            private string userId = string.Empty;

            public ReportApiController()
            {
            }

            public ReportApiController(IReportService reportService,IUserService userService)
            {
                this._reportService = reportService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }


            #region transactions
            [HttpGet]
            [ActionName("GetAllTransactionsBetweenTheSpecifiedDates")]
            public IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate)
            {
                return _reportService.GetAllTransactionsBetweenTheSpecifiedDates(lowerSpecifiedDate,upperSpecifiedDate);
            }

            [HttpGet]
            [ActionName("GenerateTransactionCurrentMonthReport")]
            public IEnumerable<Transaction> GenerateTransactionCurrentMonthReport()
            {
                return _reportService.GenerateTransactionCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateTransactionTodaysReport")]
            public IEnumerable<Transaction> GenerateTransactionTodaysReport()
            {
                return _reportService.GenerateTransactionTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateTransactionCurrentWeekReport")]
            public IEnumerable<Transaction> GenerateTransactionCurrentWeekReport()
            {
                return _reportService.GenerateTransactionCurrentWeekReport();
            }
            #endregion

        #region supplies
            [HttpPost]
            [ActionName("GetAllSuppliesBetweenTheSpecifiedDates")]
            public IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllSuppliesBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate);
            }

            [HttpGet]
            [ActionName("GenerateSupplyCurrentMonthReport")]
            public IEnumerable<Supply> GenerateSupplyCurrentMonthReport()
            {
                return _reportService.GenerateSupplyCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateSupplyTodaysReport")]
            public IEnumerable<Supply> GenerateSupplyTodaysReport()
            {
                return _reportService.GenerateSupplyTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateSupplyCurrentWeekReport")]
            public IEnumerable<Supply> GenerateSupplyCurrentWeekReport()
            {
                return _reportService.GenerateSupplyCurrentWeekReport();
            }
        #endregion
    }
}
