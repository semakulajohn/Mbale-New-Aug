using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
 public   class StoreStockDTO
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
    }
}
