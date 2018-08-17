using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
 public   class ActivityBatchCasualDTO
    {
        public long ActivityId { get; set; }
        public long CasualWorkerId { get; set; }
        public long BatchId { get; set; }
        public double Amount { get; set; }
        public System.DateTime Timestamp { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
