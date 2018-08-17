using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
 public   class StockGradeSize
    {
        public long StockId { get; set; }
        public long GradeId { get; set; }
        public long SizeId { get; set; }
        public double Quantity { get; set; }
        public System.DateTime TimeStamp { get; set; }
    }
}
