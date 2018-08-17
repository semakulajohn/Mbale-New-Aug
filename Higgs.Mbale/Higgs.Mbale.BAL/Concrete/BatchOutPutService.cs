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
 public   class BatchOutPutService : IBatchOutPutService
    {
     private long supplyStatusIdComplete = Convert.ToInt64(ConfigurationManager.AppSettings["SupplyStatusIdComplete"]);
     private long flourId = Convert.ToInt64(ConfigurationManager.AppSettings["FlourId"]);
     private long brandId = Convert.ToInt64(ConfigurationManager.AppSettings["BrandId"]);
      ILog logger = log4net.LogManager.GetLogger(typeof(BatchOutPutService));
        private IBatchOutPutDataService _dataService;
        private IUserService _userService;
      private ISupplyDataService _supplyDataService;
      private IStockService _stockService;
                

        public BatchOutPutService(IBatchOutPutDataService dataService,IUserService userService,ISupplyDataService supplyDataService,
            IStockService stockService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._supplyDataService = supplyDataService;
            this._stockService = stockService;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BatchOutPutId"></param>
        /// <returns></returns>
        public BatchOutPut GetBatchOutPut(long batchOutPutId)
        {
            var result = this._dataService.GetBatchOutPut(batchOutPutId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BatchOutPut> GetAllBatchOutPuts()
        {
            var results = this._dataService.GetAllBatchOutPuts();
            return MapEFToModel(results);
        }


        public IEnumerable<BatchOutPut> GetAllBatchOutPutsForAParticularBatch(long batchId)
        {
            var results = this._dataService.GetAllBatchOutPutsForAParticularBatch(batchId);
            return MapEFToModel(results);
        }

        #region computations
        private double ComputeBatchLoss(double brandOutPut, double flourOutPut, double quantity)
        {
            double loss;
            loss = quantity - (brandOutPut + flourOutPut);
            return loss;
        }
        private double ComputePercentageBatchLoss(double brandOutPut, double flourOutPut, double quantity)
        {
            double percentageLoss;
            var loss = ComputeBatchLoss(brandOutPut, flourOutPut, quantity);
            percentageLoss = 100 * (loss / quantity);
            return percentageLoss;

        }
        private double ComputePercentageBatchFlourOutPut(double flourOutput, double quantity)
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

       
        #endregion

        private double BatchQuantity(long batchId)
        {
            double totalQuantity = 0;
            var batchSupplies = _dataService.GetBatchSupplies(batchId);
            foreach (var batchSupply in batchSupplies)
            {
                totalQuantity = batchSupply.Quantity + totalQuantity;
            }
            return totalQuantity;
        }
        public long SaveBatchOutPut(BatchOutPut batchOutPut, string userId)
        {
            List<StockGradeSize> stockGradeSizeList = new List<StockGradeSize>();
            double batchQuantity = BatchQuantity(batchOutPut.BatchId);
            var batchOutPutDTO = new DTO.BatchOutPutDTO()
            {
                BatchId = batchOutPut.BatchId,
                BrandOutPut = batchOutPut.BrandOutPut,
                FlourOutPut = batchOutPut.FlourOutPut,
                BranchId = batchOutPut.BranchId,
                SectorId = batchOutPut.SectorId,
                Loss = ComputeBatchLoss(batchOutPut.BrandOutPut,batchOutPut.FlourOutPut,batchQuantity),
                FlourPercentage = ComputePercentageBatchFlourOutPut(batchOutPut.FlourOutPut, batchQuantity),
               BrandPercentage = ComputePercentageBatchBrandOutPut(batchOutPut.BrandOutPut,batchQuantity),
               LossPercentage = ComputePercentageBatchLoss(batchOutPut.BrandOutPut,batchOutPut.FlourOutPut,batchQuantity),
                Deleted = batchOutPut.Deleted,
                CreatedBy = batchOutPut.CreatedBy,
                CreatedOn = batchOutPut.CreatedOn
            };

           var batchOutPutId = this._dataService.SaveBatchOutPut(batchOutPutDTO, userId);

           if (batchOutPut.Grades != null)
           {
               if (batchOutPut.Grades.Any())
               {
                   List<BatchGradeSize> batchGradeSizeList = new List<BatchGradeSize>();

                   foreach (var grade in batchOutPut.Grades)
                   {   
                       var gradeId = grade.GradeId;
                       if (grade.Denominations != null)
                       {
                           if (grade.Denominations.Any())
                           {
                               foreach (var denomination in grade.Denominations)
                               {
                                   var batchGradeSize = new BatchGradeSize()
                                   {
                                       GradeId = gradeId,
                                       SizeId = denomination.DenominationId,
                                       BatchOutPutId = batchOutPutId,
                                       Quantity = denomination.Quantity,
                                       TimeStamp = DateTime.Now
                                   };
                                   batchGradeSizeList.Add(batchGradeSize);

                                   var stockGradeSize = new StockGradeSize()
                                   {
                                       GradeId = gradeId,
                                       SizeId = denomination.DenominationId,
                                       Quantity = denomination.Quantity,
                                       TimeStamp = DateTime.Now
                                   };
                                   stockGradeSizeList.Add(stockGradeSize);
                               }
                           }
                       }
                   }
                   this._dataService.PurgeBatchGradeSize(batchOutPutId);
                   this.SaveBatchGradeSizeList(batchGradeSizeList);
                   
               }
           }
           UpdateBatchSupply(batchOutPut.BatchId, supplyStatusIdComplete, userId);
           if (batchOutPut.BrandOutPut != 0 && batchOutPut.FlourOutPut !=0)
           {
               var stock = new Stock()
               {
                   SectorId = batchOutPut.SectorId,
                   BranchId = batchOutPut.BranchId,
                   BatchId = batchOutPut.BatchId,
                   InOrOut = true,
                   StoreId = batchOutPut.StoreId,
                   ProductId = brandId,
                   ProductOutPut = batchOutPut.BrandOutPut,
                   
               };
               _stockService.SaveStock(stock, userId);
               var flourStock = new Stock()
               {
                   SectorId = batchOutPut.SectorId,
                   BranchId = batchOutPut.BranchId,
                   BatchId = batchOutPut.BatchId,
                   InOrOut = true,
                   StoreId = batchOutPut.StoreId,
                   ProductId = flourId,
                   ProductOutPut = batchOutPut.FlourOutPut,
                   StockGradeSize = stockGradeSizeList

               };
               _stockService.SaveStock(flourStock, userId);
           }
                     
           return batchOutPutId;
                      
        }

     private   void UpdateBatchSupply(long batchId,long statusId,string userId)
        {
            var batchSupplies = _dataService.GetBatchSupplies(batchId);
            if (batchSupplies.Any())
            {
               
                foreach (var batchSupply in batchSupplies)
                {
                   
                    _supplyDataService.UpdateBatchSupplyWithCompletedStatus(batchSupply.SupplyId,supplyStatusIdComplete, userId);
                }
            }
               
        }
        void SaveBatchGradeSizeList(List<BatchGradeSize> batchGradeSizeList)
        {
            if (batchGradeSizeList != null)
            {
                if (batchGradeSizeList.Any())
                {
                    foreach (var batchGradeSize in batchGradeSizeList)
                    {
                        var batchGradeSizeDTO = new DTO.BatchGradeSizeDTO()
                        {
                            BatchOutPutId = batchGradeSize.BatchOutPutId,
                            GradeId = batchGradeSize.GradeId,
                            Quantity = batchGradeSize.Quantity,
                            SizeId = batchGradeSize.SizeId,
                            TimeStamp = batchGradeSize.TimeStamp
                        };
                        this.SaveBatchGradeSize(batchGradeSizeDTO);
                    }
                }
            }
        }

        void SaveBatchGradeSize(BatchGradeSizeDTO batchGradeSizeDTO)
        {
            _dataService.SaveBatchGradeSize(batchGradeSizeDTO);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BatchOutPutId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long batchOutPutId, string userId)
        {
            _dataService.MarkAsDeleted(batchOutPutId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<BatchOutPut> MapEFToModel(IEnumerable<EF.Models.BatchOutPut> data)
        {
            var list = new List<BatchOutPut>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps BatchOutPut EF object to Batch Model Object and
        /// returns the BatchOutPut model object.
        /// </summary>
        /// <param name="result">EF BatchOutPut object to be mapped.</param>
        /// <returns>BatchOutPut Model Object.</returns>
        public BatchOutPut MapEFToModel(EF.Models.BatchOutPut data)
        {
            
            var batchOutPut = new BatchOutPut()
            {
                BatchOutPutId = data.BatchOutPutId,
                BatchId = data.BatchId,
                FlourOutPut = data.FlourOutPut,
                BrandOutPut = data.BrandOutPut,
                BrandPercentage = data.BrandPercentage,
                FlourPercentage = data.FlourPercentage,
                Loss = data.Loss,
                LossPercentage = data.LossPercentage,
                BranchId = data.BranchId,
                BranchName = data.Branch != null? data.Branch.Name :"",
                SectorId = data.SectorId,
                SectorName = data.Sector != null? data.Sector.Name : "",
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
            };
                       
          
            if (data.BatchGradeSizes!= null)
            {
                if (data.BatchGradeSizes.Any())
                {
                    List<Grade> grades = new List<Grade>();
                    var distinctGrades = data.BatchGradeSizes.GroupBy(g => g.GradeId).Select(o => o.First()).ToList();
                    foreach (var batchGradeSize in distinctGrades)
                    {
                        var grade = new Grade()
                        {
                            GradeId = batchGradeSize.Grade.GradeId,
                            Value = batchGradeSize.Grade.Value,
                            CreatedOn = batchGradeSize.Grade.CreatedOn,
                            TimeStamp = batchGradeSize.Grade.TimeStamp,
                            Deleted = batchGradeSize.Grade.Deleted,
                            CreatedBy = _userService.GetUserFullName(batchGradeSize.Grade.AspNetUser),
                            UpdatedBy = _userService.GetUserFullName(batchGradeSize.Grade.AspNetUser1),
                        };
                        List<Denomination> denominations = new List<Denomination>();
                        if (batchGradeSize.Grade.BatchGradeSizes!= null)
                        {
                            if (batchGradeSize.Grade.BatchGradeSizes.Any())
                            {
                                var distinctSizes = batchGradeSize.Grade.BatchGradeSizes.GroupBy(s => s.SizeId).Select(o => o.First()).ToList();
                                foreach (var ogs in distinctSizes)
                                {
                                    var denomination = new Denomination()
                                    {
                                        DenominationId = ogs.SizeId,
                                        Value = ogs.Size != null ? ogs.Size.Value : 0,
                                        Quantity = ogs.Quantity,
                                        Rate = ogs.Size.Rate,
                                        Amount = ogs.Quantity*(Convert.ToDouble(ogs.Size.Rate)),
                                    };
                                    batchOutPut.TotalQuantity += (ogs.Quantity * ogs.Size.Value);
                                    batchOutPut.TotalBuveraCost += denomination.Amount;
                                    denominations.Add(denomination);
                                }
                            }
                            grade.Denominations = denominations;
                        }
                        grades.Add(grade);
                    }
                    batchOutPut.Grades = grades;
                   
                }
           
            }
          
            return batchOutPut;
            
           
        }



       #endregion
    }
}
