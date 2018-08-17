using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
   public class FactoryExpense
    {
        public long FactoryExpenseId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public long BatchId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public long SectorId { get; set; }
        public long BranchId { get; set; }

        public string BranchName { get; set; }
        public string SectorName { get; set; }
    }
}
