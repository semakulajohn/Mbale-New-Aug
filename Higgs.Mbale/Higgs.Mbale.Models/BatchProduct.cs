using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
public    class BatchProduct
    {
        public long BatchId { get; set; }
        public long ProductId { get; set; }
        public double OutPut { get; set; }
    }
}
