using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Higgs.Mbale.Helpers;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.Web.Controllers
{
    public class ExcelController : Controller
    {
        
        private ITransactionService _transactionService;
        private IReportService _reportService;
        private ISupplyService _supplyService;
        public ExcelController()
        {

        }

        public ExcelController(ITransactionService transactionService, IReportService reportService,
            ISupplyService supplyService)
        {
            this._transactionService = transactionService;
            this._reportService = reportService;
            this._supplyService = supplyService;
        }
        // GET: Excel
        public ActionResult Index(int id)
        {
            int reportType = id;
            string nameOfReport = string.Empty;
            List<string> headers = new List<string>();
            headers.Add("TransactionId ");
            headers.Add("Amount");
            headers.Add("TransactionSubTypeName");
            headers.Add("CreatedOn");
            headers.Add("TransactionType");
            headers.Add("Branch");

            IEnumerable<Transaction> transactionList;
            switch (reportType)
            {

                case 1://all todays transactions
                    nameOfReport = "TodaysTransactions";
                    transactionList = _reportService.GenerateTransactionTodaysReport();
                    break;
                case 2://all this months transactions
                    nameOfReport = "CurrentMonthsTransactions";
                    transactionList = _reportService.GenerateTransactionCurrentMonthReport();
                    break;
               
                case 3://transactions for this week
                    nameOfReport = "CurrentWeeksTransactions";
                    transactionList = _reportService.GenerateTransactionCurrentWeekReport();
                   break;
               
                default://Todo:: need to decide which one is the default report data
                    transactionList = _transactionService.GetAllTransactions();
                    break;
            }
            List<List<string>> cellValues = new List<List<string>>();
            foreach (var w in transactionList)
            {
               
                var sxr = new List<string>();
                sxr.Add(w.TransactionId.ToString());
                sxr.Add(w.Amount.ToString());
                sxr.Add(w.TransactionSubTypeName);
                sxr.Add(w.CreatedOn.ToString());
                sxr.Add(w.TransactionTypeName);
                sxr.Add(w.BranchName);

                cellValues.Add(sxr);
            }
            var data = new ExcelData();
            data.Headers = headers;
            data.DataRows = cellValues;

            var file = new ExcelWriter();
            var excelFileContentInBytes = file.GenerateExcelFile(data);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nameOfReport + "_Report_" + DateTime.Now.ToString("yyyy-MM-dd-mm-ss") + ".xlsx");
            return new FileContentResult(excelFileContentInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult Supply(int id)
        {
            int reportType = id;
            string nameOfReport = string.Empty;
            List<string> headers = new List<string>();
            headers.Add("SupplyNumber ");
            headers.Add("Quantity");
            headers.Add("Price");
            headers.Add("Amount");
            headers.Add("BranchName");
            headers.Add("SupplyDate");
            headers.Add("TruckerNumber");
            headers.Add("SupplierName");
          

            IEnumerable<Supply> supplyList;
            switch (reportType)
            {

                case 1://all todays Supplies
                    nameOfReport = "TodaysSupplies";
                    supplyList = _reportService.GenerateSupplyTodaysReport();
                    break;
                case 2://all this months Supplies
                    nameOfReport = "CurrentMonthsSupplies";
                    supplyList = _reportService.GenerateSupplyCurrentMonthReport();
                    break;
               
                case 3://Supplies for this week
                    nameOfReport = "CurrentWeeksSupplies";
                    supplyList = _reportService.GenerateSupplyCurrentWeekReport();
                    break;
                
                default://Todo:: need to decide which one is the default report data
                    supplyList = _supplyService.GetAllSupplies();
                    break;
            }
            List<List<string>> cellValues = new List<List<string>>();
            foreach (var w in supplyList)
            {
               
                var sxr = new List<string>();
                sxr.Add(w.SupplyNumber.ToString());
                sxr.Add(w.Quantity.ToString());
                sxr.Add(w.Price.ToString());
                sxr.Add(w.Amount.ToString());
                sxr.Add(w.BranchName);
                sxr.Add(w.SupplyDate.ToString());
                sxr.Add(w.TruckNumber);
                sxr.Add(w.SupplierName);
                

                cellValues.Add(sxr);
            }
            var data = new ExcelData();
            data.Headers = headers;
            data.DataRows = cellValues;

            var file = new ExcelWriter();
            var excelFileContentInBytes = file.GenerateExcelFile(data);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nameOfReport + "_Report_" + DateTime.Now.ToString("yyyy-MM-dd-mm-ss") + ".xlsx");
            return new FileContentResult(excelFileContentInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}