using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
  public  class BatchDTO
    {
        public long BatchId { get; set; }
        public long SectorId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public long BranchId { get; set; }
        public double Loss { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<double> LossPercentage { get; set; }
        public Nullable<double> FlourPercentage { get; set; }
        public Nullable<double> BrandPercentage { get; set; }

        
        public List<long> Grades { get; set; }

        public double BrandOutPut { get; set; }
        public double FlourOutPut { get; set; }

    }
}
