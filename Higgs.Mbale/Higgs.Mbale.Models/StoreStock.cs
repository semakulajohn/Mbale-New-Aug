using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
 public   class StoreStock
    {
        public long StoreStockId { get; set; }
        public long StockId { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        public double StockBalance { get; set; }
        public long StoreId { get; set; }
        public long BranchId { get; set; }
        public long SectorId { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public double StartStock { get; set; }

        public string BranchName { get; set; }
        public string StoreName { get; set; }
        public string SectorName { get; set; }
        public string ProductName { get; set; }
        public string BatchNumber { get; set; }
        public string StockInOrOut { get; set; }
    }
}
