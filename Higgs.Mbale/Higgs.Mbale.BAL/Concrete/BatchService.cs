using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;
using log4net;
using System.Configuration;

namespace Higgs.Mbale.BAL.Concrete
{
  public  class BatchService : IBatchService
    {
      private string supplyStatusIdInProgress = ConfigurationManager.AppSettings["SupplyStatusIdInProgress"];
         ILog logger = log4net.LogManager.GetLogger(typeof(BatchService));
        private IBatchDataService _dataService;
        private IUserService _userService;
        private ISupplyDataService _supplyDataService;
        private IFactoryExpenseService _factoryExpenseService;
        private ILabourCostService _labourCostService;
        private IOtherExpenseService _otherExpenseService;
        private IMachineRepairService _machineRepairService;
        private IBatchOutPutService _batchOutPutService;
        private ISupplyService _supplyService;
       
        

        public BatchService(IBatchDataService dataService,IUserService userService,ISupplyDataService supplyDataService,
            IFactoryExpenseService factoryExpenseService,ILabourCostService labourCostService,
            IMachineRepairService machineRepairService,IOtherExpenseService otherExpenseService,
            IBatchOutPutService batchOutPutService,ISupplyService supplyService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._supplyDataService = supplyDataService;
            this._factoryExpenseService = factoryExpenseService;
            this._labourCostService = labourCostService;
            this._machineRepairService = machineRepairService;
            this._otherExpenseService = otherExpenseService;
            this._batchOutPutService = batchOutPutService;
            this._supplyService = supplyService;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns></returns>
        public Batch GetBatch(long batchId)
        {
            var result = this._dataService.GetBatch(batchId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Batch> GetAllBatches()
        {
            var results = this._dataService.GetAllBatches();
            return MapEFToModel(results);
        }


        public IEnumerable<Batch> GetAllBatchesForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllBatchesForAParticularBranch(branchId);
            return MapEFToModel(results);
        }

        #region computations
        private double ComputeBatchLoss(double brandOutPut, double flourOutPut, double quantity)
        {
            double loss;
            loss = quantity - (brandOutPut + flourOutPut);
            return loss;
        }
        private double ComputePercentageBatchLoss(double brandOutPut,double flourOutPut, double quantity)
        {
            double percentageLoss;
            var loss = ComputeBatchLoss(brandOutPut, flourOutPut, quantity);
           percentageLoss = 100 * (loss / quantity);
            return percentageLoss;

        }
        private double ComputePercentageBatchFlourOutPut( double flourOutput, double quantity)
        {
            double percentageFlour;
            
            percentageFlour = 100 * (flourOutput / quantity);
            return percentageFlour;

        }
        private double ComputePercentageBatchBrandOutPut(double brandOutPut, double quantity)
        {
            double percentageBrand;
           
            percentageBrand = 100 * (brandOutPut / quantity);
            return percentageBrand;

        }

        private double ComputeTotalProductionCost(double millingCharge, double labourCost, double buveraCost)
        {
            double totalProductonCost = 0;
            totalProductonCost = millingCharge + labourCost + buveraCost;
            return totalProductonCost;
        }

        private double ComputeMillingChargeBalance(double millingCharge, double factoryExpenses)
        {
            double millingChargeBalance = 0;
            millingChargeBalance = millingCharge - factoryExpenses;
            return millingChargeBalance;
        }
        #endregion
        private IEnumerable<FactoryExpense> GetAllFactoryExpensesForABatch(long batchId)
        {
            var results = _factoryExpenseService.GetAllFactoryExpensesForAParticularBatch(batchId);
            return results;
        }

        private IEnumerable<OtherExpense> GetAllOtherExpensesForABatch(long batchId)
        {
            var results = _otherExpenseService.GetAllOtherExpensesForAParticularBatch(batchId);
            return results;
        }
        private IEnumerable<LabourCost> GetAllLabourCostsForABatch(long batchId)
        {
            var results = _labourCostService.GetAllLabourCostsForAParticularBatch(batchId);
            return results;
        }
        private IEnumerable<BatchOutPut> GetAllBatchOutPutsForABatch(long batchId)
        {
            var results = _batchOutPutService.GetAllBatchOutPutsForAParticularBatch(batchId);
            return results;
        }

        private IEnumerable<MachineRepair> GetAllMachineRepairsForABatch(long batchId)
        {
            var results = _machineRepairService.GetAllMachineRepairsForAParticularBatch(batchId);
            return results;
        }
        public long SaveBatch(Batch batch, string userId)
        {
            var batchDTO = new DTO.BatchDTO()
            {
                BatchId = batch.BatchId,
                Name = batch.Name,
                SectorId = batch.SectorId,
                Quantity = batch.Quantity,
                BranchId = batch.BranchId,
                Deleted = batch.Deleted,
                CreatedBy = batch.CreatedBy,
                CreatedOn = batch.CreatedOn
            };

           var batchId = this._dataService.SaveBatch(batchDTO, userId);

          
           if (batch.Supplies.Any())
           {
               EF.Models.Supply supplyObject = new EF.Models.Supply();
               
               foreach (var batchSupply in batch.Supplies) 
               {
                   var batchSupplyDTO = new BatchSupplyDTO()
                   {
                       BatchId = batchId,
                       SupplyId = batchSupply.SupplyId,
                       Quantity = batchSupply.Quantity,
                   };
                   this._dataService.PurgeBatchSupply(batchId,batchSupply.SupplyId);
                   this._dataService.SaveBatchSupply(batchSupplyDTO);
 
                   var storeMaizeStock = new StoreMaizeStock()
                   {
                       SupplyId = batchSupply.SupplyId,
                       Quantity = batchSupply.Quantity,
                       StoreId = batch.StoreId,
                       BranchId = batch.BranchId,
                       SectorId = batch.SectorId,

                   };
                 _supplyService.SaveStoreMaizeStock(storeMaizeStock, false);
               
                 supplyObject =  this._supplyDataService.GetSupply(batchSupply.SupplyId);
                 var supply = new SupplyDTO()
                 {
                      Quantity = supplyObject.Quantity,
                    SupplyDate = supplyObject.SupplyDate,
                    SupplyNumber =  supplyObject.SupplyNumber,
                    BranchId = supplyObject.BranchId,
                    SupplierId = supplyObject.SupplierId,
                    Amount = supplyObject.Amount,
                    StoreId = supplyObject.StoreId,
                    TruckNumber = supplyObject.TruckNumber,
                    Price = supplyObject.Price,
                    WeightNoteNumber = supplyObject.WeightNoteNumber,
                    BagsOfStones = supplyObject.BagsOfStones,
                    NormalBags = supplyObject.NormalBags,
                    MoistureContent = supplyObject.MoistureContent,
                    Deleted = supplyObject.Deleted,
                    DeletedBy = supplyObject.DeletedBy,
                    DeletedOn = supplyObject.DeletedOn,
                    StatusId = Convert.ToInt64(supplyStatusIdInProgress),
                     Used = true,
                     SupplyId = supplyObject.SupplyId
                 };
                 this._supplyDataService.SaveSupply(supply, userId);
               }

            
           }
           return batchId;
                      
        }

      
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long batchId, string userId)
        {
            _dataService.MarkAsDeleted(batchId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Batch> MapEFToModel(IEnumerable<EF.Models.Batch> data)
        {
            var list = new List<Batch>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Batch EF object to Batch Model Object and
        /// returns the Batch model object.
        /// </summary>
        /// <param name="result">EF Batch object to be mapped.</param>
        /// <returns>Batch Model Object.</returns>
        public Batch MapEFToModel(EF.Models.Batch data)
        {
            
            var batch = new Batch()
            {
                BatchId = data.BatchId,
                Name = data.Name,
                Quantity = data.Quantity,
                BranchId = data.BranchId,
                
                BranchName = data.Branch != null ? data.Branch.Name : "",
                SectorName = data.Sector != null ? data.Sector.Name : "",
                SectorId = data.SectorId,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                BranchMillingChargeRate = data.Branch != null? data.Branch.MillingChargeRate:0,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
            };
            

            var batchOutPuts = GetAllBatchOutPutsForABatch(data.BatchId);
            List<BatchOutPut> batchOutPutList = new List<BatchOutPut>();
            if (batchOutPuts.Any())
            {
                foreach (var outPut in batchOutPuts)
                {
                    var batchOutPut = new BatchOutPut()
                    {
                        Grades = outPut.Grades,
                        TotalBuveraCost = outPut.TotalBuveraCost,
                        TotalQuantity = outPut.TotalQuantity,
                        BrandOutPut = outPut.BrandOutPut,
                        FlourPercentage = outPut.FlourPercentage,
                        BrandPercentage = outPut.BrandPercentage,
                        FlourOutPut = outPut.FlourOutPut,
                        LossPercentage = outPut.LossPercentage,
                        Loss = outPut.Loss,

                    };
                    batchOutPutList.Add(batchOutPut);
                    batch.TotalBuveraCost = batchOutPut.TotalBuveraCost;
                    batch.TotalQuantity = batchOutPut.TotalQuantity;
                    batch.FlourOutPut = batchOutPut.FlourOutPut;
                    batch.LossPercentage = batchOutPut.LossPercentage;
                    batch.Loss =Convert.ToDouble(batchOutPut.Loss);
                    batch.BrandOutPut = batchOutPut.BrandOutPut;
                    batch.BrandPercentage = batchOutPut.BrandPercentage;
                    batch.FlourPercentage = batchOutPut.FlourPercentage;
                    batch.Grades = batchOutPut.Grades;
                  }
             
            }
            batch.MillingCharge = batch.BranchMillingChargeRate * batch.FlourOutPut;

            var otherExpenses = GetAllOtherExpensesForABatch(data.BatchId);
            double otherExpenseCost = 0;
            List<OtherExpense> otherExpenseList = new List<OtherExpense>();
            if (otherExpenses.Any())
            {
                foreach (var other in otherExpenses)
                {
                    var otherExpense = new OtherExpense()
                    {
                        Amount = other.Amount,
                        Description = other.Description,

                    };
                    otherExpenseList.Add(otherExpense);
                    otherExpenseCost = otherExpenseCost + other.Amount;
                }
                batch.TotalOtherExpenseCost = otherExpenseCost;
                batch.OtherExpenses = otherExpenseList;
            }

            var factoryExpenses = GetAllFactoryExpensesForABatch(data.BatchId);
            double totalFactoryExpense = 0;
            double factoryExpenseCost = 0;
            List<FactoryExpense> factoryExpenseList = new List<FactoryExpense>();
            if (factoryExpenses.Any())
            {
                foreach (var item in factoryExpenses)
                {
                    var factoryExpense = new FactoryExpense()
                    {
                        Amount = item.Amount,
                        Description = item.Description,

                    };
                    factoryExpenseList.Add(factoryExpense);
                    factoryExpenseCost = factoryExpenseCost + item.Amount;  
                }
                batch.FactoryExpenseCost = factoryExpenseCost;
                batch.FactoryExpenses = factoryExpenseList;
            }
            var machineRepairs = GetAllMachineRepairsForABatch(data.BatchId);
            double machineCosts = 0;
            List<MachineRepair> machineRepairList = new List<MachineRepair>();
            if (machineRepairs.Any())
            {
                foreach (var repair in machineRepairs)
                {
                    var machineRepair = new MachineRepair()
                    {
                        Amount = repair.Amount,
                        Description = repair.Description,
                    };
                    machineRepairList.Add(machineRepair);
                    machineCosts = machineRepair.Amount + machineCosts;
                }
                batch.MachineRepairs = machineRepairList;
                batch.TotalMachineCost = machineCosts;
            }
            totalFactoryExpense = batch.TotalMachineCost + batch.FactoryExpenseCost;
            batch.TotalFactoryExpenseCost = totalFactoryExpense;
            batch.MillingChargeBalance = ComputeMillingChargeBalance(batch.MillingCharge, batch.TotalFactoryExpenseCost);

            var labourCosts = GetAllLabourCostsForABatch(data.BatchId);
            double totalLabourCosts = 0;
            List<LabourCost> labourCostList = new List<LabourCost>();
            if (labourCosts.Any())
            {
                foreach (var labour in labourCosts)
                {
                    var labourCost = new LabourCost()
                    {
                        ActivityName = labour.ActivityName,
                        Amount = labour.Amount,
                        Quantity = labour.Quantity,
                        Rate = labour.Rate,
                    };
                    labourCostList.Add(labourCost);
                    totalLabourCosts = totalLabourCosts + labour.Amount;
                }
                batch.TotalLabourCosts = totalLabourCosts;
                batch.LabourCosts = labourCostList;
            }

            if(data.BatchSupplies != null){
                if (data.BatchSupplies.Any())
                {
                    double totalSupplyAmount = 0;
                    List<Supply> supplies = new List<Supply>();
                    var batchSupplies = data.BatchSupplies.AsQueryable().Where(m =>m.BatchId == data.BatchId);
                    foreach (var batchSupply in batchSupplies)
                    {
                        var supply = new Supply()
                        {
                            SupplyId = batchSupply.Supply.SupplyId,
                            Quantity = batchSupply.Supply.Quantity,
                            SupplierId = batchSupply.Supply.SupplierId,
                            Price = batchSupply.Supply.Price,
                            Amount = batchSupply.Supply.Amount,
                           SupplierName = _userService.GetUserFullName(batchSupply.Supply.AspNetUser2),
                        };
                        supplies.Add(supply);
                        totalSupplyAmount = totalSupplyAmount + supply.Amount;
                    }
                    batch.Supplies = supplies;
                    batch.TotalSupplyAmount = totalSupplyAmount;
                }
            
            
                batch.TotalProductionCost = ComputeTotalProductionCost(batch.MillingCharge, batch.TotalLabourCosts, batch.TotalBuveraCost);
            }
          
            return batch;
            
           
        }



       #endregion
    }
}
