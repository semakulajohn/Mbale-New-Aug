using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
 public   class LabourCost
    {
        public long LabourCostId { get; set; }
        public double Quantity { get; set; }
        public double Amount { get; set; }
        public double Rate { get; set; }
        public long BatchId { get; set; }
        public long ActivityId { get; set; }
        public long SectorId { get; set; }
        public long BranchId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public string ActivityName { get; set; }
        public string BranchName { get; set; }
        public string SectorName { get; set; }
    }
}
