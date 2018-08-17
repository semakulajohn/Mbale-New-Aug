using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
  public  class BatchSupply
    {
        public long BatchId { get; set; }
        public long SupplyId { get; set; }
        public double Quantity { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
