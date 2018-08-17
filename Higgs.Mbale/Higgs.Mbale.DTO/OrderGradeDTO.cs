using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
 public   class OrderGradeDTO
    {
        public long OrderId { get; set; }
        public long GradeId { get; set; }
        public System.DateTime TimeStamp { get; set; }       
    }
}
