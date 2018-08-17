using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
 public   class BatchGradeDTO
    {
        public long BatchId { get; set; }
        public long GradeId { get; set; }
        public System.DateTime TimeStamp { get; set; }
    }
}
