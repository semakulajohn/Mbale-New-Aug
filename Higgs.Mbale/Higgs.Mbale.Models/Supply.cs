using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
 public   class Supply
    {
        public long SupplyId { get; set; }
        public long BranchId { get; set; }
        public double Amount { get; set; }
        public string TruckNumber { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public long SupplyNumber { get; set; }
        public string SupplierId { get; set; }
        public System.DateTime SupplyDate { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool Used { get; set; }
        public string WeightNoteNumber { get; set; }
        public Nullable<double> MoistureContent { get; set; }
        public double BagsOfStones { get; set; }
        public double NormalBags { get; set; }
        public bool IsPaid { get; set; }
        public long StatusId { get; set; }
        public long StoreId { get; set; }
    
        public string BranchName { get; set; }
        public string SupplierName { get; set; }
        public string StatusName { get; set; }
        public string StoreName { get; set; }
    }
}
