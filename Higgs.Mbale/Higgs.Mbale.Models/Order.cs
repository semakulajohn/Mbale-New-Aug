using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
  public  class Order
    {
        public long OrderId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public double TotalQuantity { get; set; }
        public long StatusId { get; set; }
        public string CustomerId { get; set; }
        public long ProductId { get; set; }
        public long BranchId { get; set; }
        public string ProductName { get; set; }
        public string BranchName { get; set; }
        public string StatusName { get; set; }
        public string CustomerName { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public List<Grade> Grades { get; set; }
    }
}
