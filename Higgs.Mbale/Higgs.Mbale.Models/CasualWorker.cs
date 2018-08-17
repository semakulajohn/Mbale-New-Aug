using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
  public  class CasualWorker
    {
        public long CasualWorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long BranchId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public string NINNumber { get; set; }
        public string NextOfKeen { get; set; }
        public string EmailAddress { get; set; }
        public string UniqueNumber { get; set; }

        public string BranchName { get; set; }
    }
}
