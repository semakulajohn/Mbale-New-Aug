using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
   public class Delivery
    {
        public long DeliveryId { get; set; }
        public string CustomerName { get; set; }
        public string DriverName { get; set; }
        public double DeliveryCost { get; set; }
        public long OrderId { get; set; }
        public string VehicleNumber { get; set; }
        public long MediaId { get; set; }
        public long BranchId { get; set; }
        public string Location { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public long TransactionSubTypeId { get; set; }
        public long SectorId { get; set; }
        public string DriverNIN { get; set; }
        
       public string TransactionSubTypeName { get; set; }
       public string ProductName { get; set; }
       public string BranchName { get; set; }
       public string SectorName { get; set; }
       public string CustomerId { get; set; }
    }
}
