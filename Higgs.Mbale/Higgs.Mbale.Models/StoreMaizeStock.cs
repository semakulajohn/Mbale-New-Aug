using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
public    class StoreMaizeStock
    {
        public long StoreMaizeStockId { get; set; }
        public long SupplyId { get; set; }
        public double StartStock { get; set; }
        public double Quantity { get; set; }
        public double StockBalance { get; set; }
        public long StoreId { get; set; }
        public long BranchId { get; set; }
        public long SectorId { get; set; }
        public System.DateTime TimeStamp { get; set; }

        public string BranchName { get; set; }
        public string SectorName { get; set; }
        public string StoreName { get; set; }
        public string SupplyNumber { get; set; }
    }
}
