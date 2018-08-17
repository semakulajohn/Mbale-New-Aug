using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
  public  class StoreGradeSizeDTO
    {
       
        public long GradeId { get; set; }
        public long StoreId { get; set; }
        public long SizeId { get; set; }
        public double Quantity { get; set; }
        public System.DateTime TimeStamp { get; set; }
    
    }
}
