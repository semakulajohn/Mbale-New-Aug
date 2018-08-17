using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
   public class Stock
    {
        public List<Grade> Grades { get; set; }
        public List<StockGradeSize> StockGradeSize { get; set; }
        public long StockId { get; set; }
        public long SectorId { get; set; }
        public long BatchId { get; set; }
        public long BranchId { get; set; }
        public long ProductId { get; set; }
        public bool InOrOut { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public double ProductOutPut { get; set; }
        public long StoreId { get; set; }

        public string BranchName { get; set; }
        public string ProductName { get; set; }
        public string SectorName { get; set; }
        public string BatchNumber { get; set; }
        public string StockInOrOut { get; set; }
        public string StoreName { get; set; }
       
    }
}
