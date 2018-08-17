using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
  public  class Batch
    {
        public long BatchId { get; set; }
        public long SectorId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double TotalQuantity { get; set; }
        public long BranchId { get; set; }
        public long StoreId { get; set; }
        
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public double Loss { get; set; }
        public double BrandOutPut { get; set; }
        public double FlourOutPut { get; set; }
        public Nullable<double> LossPercentage { get; set; }
        public Nullable<double> FlourPercentage { get; set; }
        public Nullable<double> BrandPercentage { get; set; }


        public double BranchMillingChargeRate { get; set; }
        public double MillingCharge { get; set; }
        public double TotalSupplyAmount { get; set; }
        public string BranchName { get; set; }
        public string SupplierNames { get; set; }
        public string SectorName { get; set; }
        public double TotalBuveraCost { get; set; }
        public double TotalFactoryExpenseCost { get; set; }
        public double FactoryExpenseCost { get; set; }
        public double TotalMachineCost { get; set; }
        public double TotalLabourCosts { get; set; }
        public double TotalOtherExpenseCost { get; set; }
        public double MillingChargeBalance { get; set; }
        public double TotalProductionCost { get; set; }

        public List<Supply> Supplies { get; set; }
        public List<long> SupplyIds { get; set; }
        public List<Grade> Grades { get; set; }

        public List<FactoryExpense> FactoryExpenses { get; set; }
        public List<LabourCost> LabourCosts { get; set; }
        public List<MachineRepair> MachineRepairs { get; set; }
        public List<OtherExpense> OtherExpenses { get; set; }
        public List<BatchOutPut> BatchOutPuts { get; set; }
    }
}
