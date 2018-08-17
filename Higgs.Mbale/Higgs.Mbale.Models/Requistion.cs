using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
 public   class Requistion
    {
        public long RequistionId { get; set; }
        public long StatusId { get; set; }
        public long BranchId { get; set; }
        public string ApprovedById { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
        public string RequistionNumber { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public string BranchName { get; set; }
        public string StatusName { get; set; }
        public string ApprovedByName { get; set; }
    }
}
