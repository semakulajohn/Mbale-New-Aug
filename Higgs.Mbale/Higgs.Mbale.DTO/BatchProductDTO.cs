using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
 public   class BatchProductDTO
    {
        public long BatchId { get; set; }
        public long ProductId { get; set; }
        public double OutPut { get; set; }
        public System.DateTime TimeStamp { get; set; }
    }
}
