using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Higgs.Mbale.Models
{
 public   class MultipleSupplies
    {
        public IEnumerable<Supply> Supplies { get; set; }
        public AccountTransactionActivity AccountActivity { get; set; }
    }
}
