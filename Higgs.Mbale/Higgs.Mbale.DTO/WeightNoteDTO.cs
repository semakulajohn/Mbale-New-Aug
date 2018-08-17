using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
  public  class WeightNoteDTO
    {
        public long WeightNoteId { get; set; }
        public string SupplierId { get; set; }
        public string TruckNumber { get; set; }
        public double MoistureContent { get; set; }
        public int RejectedBags { get; set; }
        public int WeightNoteNumber { get; set; }
        public int NumberOfBags { get; set; }
        public double GrossWeight { get; set; }
        public long BranchId { get; set; }
        public long BatchId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    }
}
